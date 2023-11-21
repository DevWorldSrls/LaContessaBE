# LaContessaBE
This microservice manage a APIs call from Flutter project

# Important Note!
Until we release a first version of this BE, replace <migration-name> with "Initial" and remember to clean the contents of the folder "MigrationScripts" in project "DevWorld.LaContessa.Persistance.Migrations"

# Create migrations
1. Go to Package Manager Console
2. Execute this command to add a new migration (replace '<migration-name>'):
   ```shell
   add-migration <migration-name> -Project "DevWorld.LaContessa.Persistance.Migrations" -StartupProject "DevWorld.LaContessa.API" -Context LaContessaDbContext -o MigrationsScripts
   ```
# Apply migrations
1. Go to Package Manager Console
2. Execute this command to update your database:
   ```shell
   update-database -args "UserID=postgres;Password=lacontessa;Host=localhost;Port=5432;Database=lacontessadb" -Project "DevWorld.LaContessa.Persistance.Migrations" -StartupProject "DevWorld.LaContessa.API" -Context LaContessaDbContext
   ```
   
# Deploy on Google Cloud
List of command necessary to deploy service on GPC

## Create a repository
The following command list need to be performed only first time.

- Set the PROJECT_ID environment variable (*Replace PROJECT_ID*):
  export PROJECT_ID=PROJECT_ID
- Confirm that the PROJECT_ID environment variable has the correct value:
  echo $PROJECT_ID
- Set project ID for the Google Cloud CLI:
  gcloud config set project $PROJECT_ID
- Create the repository (*Replace REGION and repository name*):
  gcloud artifacts repositories create test-repo \
   --repository-format=docker \
   --location=REGION \
   --description="Docker repository"
- To see a list of available locations:
  gcloud artifacts locations list
  
## Building the container image
The following command list need to be performed only first time.

- Clone this repo with Cloud Shell:
  git clone https://github.com/DevWorldSrls/LaContessaBE.git
- Go in the directory that contain Dockerfile
- Build and tag the Docker image (*Replace REGION, repository name and app name*):
  docker build -t REGION-docker.pkg.dev/${PROJECT_ID}/test-repo/test-app:v1 -f Dockerfile ..
- Verify that the build was successful:
  docker images
- Add IAM policy bindings (*Replace REGION, PROJECT_NUMBER and repository name*):
  gcloud artifacts repositories add-iam-policy-binding test-repo \
    --location=REGION \
    --member=serviceAccount:PROJECT_NUMBER-compute@developer.gserviceaccount.com \
    --role="roles/artifactregistry.reader"
- Test your container image (*Replace REGION, repository name and app name*):
  docker run --rm -p 8080:8080 REGION-docker.pkg.dev/${PROJECT_ID}/test-repo/test-app:v1

## Pushing the Docker image to Artifact Registry
The following command list need to be performed only first time.

- Configure the Docker command-line tool to authenticate to Artifact Registry (*Replace REGION*):
  gcloud auth configure-docker REGION-docker.pkg.dev
- Push the Docker image (*Replace REGION, PROJECT_ID, repository name and app name*):
  docker push REGION-docker.pkg.dev/${PROJECT_ID}/test-repo/test-app:v1

## Creating a GKE cluster
The following command list need to be performed only first time.

- Set your Compute Engine region (*Replace REGION*):
  gcloud config set compute/region REGION
- Create a cluster (*Replace cluster name*):
  gcloud container clusters create-auto test-cluster

## Deploying the app to GKE
The following command list need to be performed only first time.

- Ensure that you are connected to your GKE cluster (*Replace REGION*): 
  gcloud container clusters get-credentials test-cluster --region REGION
- Create a Kubernetes Deployment (*Replace REGION, PROJECT_ID, deployment name, repository name and app name*):
  kubectl create deployment test-app --image=REGION-docker.pkg.dev/${PROJECT_ID}/test-repo/test-app:v1
- Set the baseline number of Deployment replicas (*Replace deployment name*):
  kubectl scale deployment test-app --replicas=3
- Create a HorizontalPodAutoscaler (*Replace deployment name*):
  kubectl autoscale deployment test-app --cpu-percent=80 --min=1 --max=5
- To see the Pods created, run the following command:
  kubectl get pods

## Exposing the app deployed to the internet
The following command list need to be performed only first time.

- Generate a Kubernetes Service (*Replace deployment name and service name*):
  kubectl expose deployment test-app --name=test-app-service --type=LoadBalancer --port 80 --target-port 8080
- To get the Service details:
  kubectl get service
- Copy the EXTERNAL_IP address to the clipboard

## Deploying a new version of the app

- Build and tag a new docker image (*Replace REGION, repository name and app name*):
  docker build -t REGION-docker.pkg.dev/${PROJECT_ID}/test-repo/test-app:v2 -f Dockerfile ..
- Push the image to Artifact Registry (*Replace REGION, repository name and app name*):
  docker push REGION-docker.pkg.dev/${PROJECT_ID}/test-repo/test-app:v2
- Apply a rolling update to the existing Deployment (*Replace REGION, deployment name, repository name and app name*):
  kubectl set image deployment/test-app test-app=REGION-docker.pkg.dev/${PROJECT_ID}/test-repo/test-app:v2
- Watch the running Pods:
  watch kubectl get pods

## Clean up
- Delete the Service:
  kubectl delete service hello-app-service
- Delete the cluster (*Replace cluster name*):
  gcloud container clusters delete test-cluster --region REGION
- Delete your container images (*Replace REGION, PROJECT_ID, repository name and app name*):
  gcloud artifacts docker images delete \
    REGION-docker.pkg.dev/${PROJECT_ID}/test-repo/test-app:v1 \
    --delete-tags --quiet
  gcloud artifacts docker images delete \
    REGION-docker.pkg.dev/${PROJECT_ID}/test-repo/test-app:v2 \
    --delete-tags --quiet
