#!/bin/bash

# imageName="ComplianceWebApi"
# tag="beta2"
# registry="neptuneimages.azurecr.io"

imageName=${imageName}
tag=${tag}
registry=$(registry)
# var imageName = readEnvironmentVariable('imageName')
# var tag = readEnvironmentVariable('tag')
# var tag = readEnvironmentVariable('registry')


echo "Login to Azure Container Registry"
accessToken=$(az acr login --name $registry --expose-token --output tsv --query accessToken)
docker login $registry --username 00000000-0000-0000-0000-000000000000 --password $accessToken

echo "Building Images with Tag '${imageName}:${tag}'"
docker build -t ${registry}/${imageName}:${tag} .

echo "Pushing to '$registry'"
docker push ${registry}/${imageName}:${tag}
