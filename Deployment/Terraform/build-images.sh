#!/usr/bin/env zsh

#Build product service image
docker build -t product-service:product-service-v1 -f ../../ProductService/Deployment/Dockerfile ../../

docker tag product-service:product-service-v1 $1.dkr.ecr.us-east-1.amazonaws.com/ecom:product-service-v1

docker push $1.dkr.ecr.us-east-1.amazonaws.com/ecom:product-service-v1

#Build user service image
docker build -t user-service:user-service-v1 -f ../../UserService/Deployment/Dockerfile ../../

docker tag user-service:user-service-v1 $1.dkr.ecr.us-east-1.amazonaws.com/ecom:user-service-v1

docker push $1.dkr.ecr.us-east-1.amazonaws.com/ecom:user-service-v1

#Build cart service image
docker build -t cart-service:cart-service-v1 -f ../../CartService/Deployment/Dockerfile ../../ --build-arg aws_account_id=$1

docker tag cart-service:cart-service-v1 $1.dkr.ecr.us-east-1.amazonaws.com/ecom:cart-service-v1

docker push $1.dkr.ecr.us-east-1.amazonaws.com/ecom:cart-service-v1

#Build order service image
docker build -t order-service:order-service-v1 -f ../../OrderService/Deployment/Dockerfile ../../ --build-arg aws_account_id=$1

docker tag order-service:order-service-v1 $1.dkr.ecr.us-east-1.amazonaws.com/ecom:order-service-v1

docker push $1.dkr.ecr.us-east-1.amazonaws.com/ecom:order-service-v1