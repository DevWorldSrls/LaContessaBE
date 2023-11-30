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
  - export PROJECT_ID=PROJECT_ID
- Confirm that the PROJECT_ID environment variable has the correct value:
  - echo $PROJECT_ID
- Set project ID for the Google Cloud CLI:
  - gcloud config set project $PROJECT_ID
- Create the repository (*Replace REGION and repository name*):
  - gcloud artifacts repositories create test-repo \
   --repository-format=docker \
   --location=REGION \
   --description="Docker repository"
- To see a list of available locations:
  - gcloud artifacts locations list
  
## Building the container image
The following command list need to be performed only first time.

- Clone this repo with Cloud Shell:
  - git clone https://github.com/DevWorldSrls/LaContessaBE.git
- Go in the deploy directory that contain docker-compose.yaml file
- Run build command:
  - docker-compose build
- Verify that the build was successful:
  - docker images
- Add IAM policy bindings (*Replace REGION, PROJECT_NUMBER and repository name*):
  - gcloud artifacts repositories add-iam-policy-binding test-repo \
    --location=REGION \
    --member=serviceAccount:PROJECT_NUMBER-compute@developer.gserviceaccount.com \
    --role="roles/artifactregistry.reader"

## Pushing the Docker image to Artifact Registry
The following command list need to be performed only first time.

- Configure the Docker command-line tool to authenticate to Artifact Registry (*Replace REGION*):
  - gcloud auth configure-docker REGION-docker.pkg.dev
- Push the Docker images in repository (*Replace REGION, repository name*):
  - docker push REGION-docker.pkg.dev/${PROJECT_ID}/test-repo/app:v1
  - docker push REGION-docker.pkg.dev/${PROJECT_ID}/test-repo/setup:v1

## Deploy image in Cloud Run
- Create a Job and select Container Image URL of setup present in artifact repository
- Check "Execute job immediately" and click create (This should run the setupJob that apply EF migration to deployed DB
- In Cloud Shell create service of API (*Replace REGION, repository name*):
  - gcloud run deploy app-api --image REGION-docker.pkg.dev/${PROJECT_ID}/test-repo/app:v1 --platform managed --region REGION --port 80

## Deploying a new version of the app
TODO

# Download deployed image
- Open terminal and enable auth for docker:
  - gcloud auth configure-docker us-central1-docker.pkg.dev
- Pull image deployed by tag:
  - docker pull \
    us-central1-docker.pkg.dev/lacontessa/lacontessa-be/devworld.lacontessa.api:v1
- Create container with image just pulled with specific port (ex. 9889)
- Open browser and go to swagger page:
  - http://localhost:9889/swagger/index.html
