#!/usr/bin/env zsh

#Build product service image
docker build -t product-service:product-service-v1 -f ../../ProductService/Deployment/Dockerfile ../../

docker tag product-service:product-service-v1 $1.dkr.ecr.us-east-1.amazonaws.com/ecom:product-service-v1

docker push $1.dkr.ecr.us-east-1.amazonaws.com/ecom:product-service-v1

#Build user service image
docker build -t user-service:user-service-v1 -f ../../UserService/Deployment/Dockerfile ../../

docker tag user-service:user-service-v1 $1.dkr.ecr.us-east-1.amazonaws.com/ecom:user-service-v1

docker push $1.dkr.ecr.us-east-1.amazonaws.com/ecom:user-service-v1
