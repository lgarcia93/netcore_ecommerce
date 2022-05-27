resource "aws_vpc" "ecommerce_vpc" {
  cidr_block = "10.0.0.0/16"
  enable_dns_support = true #gives you an internal domain name
  enable_dns_hostnames = true #gives you an internal host name
  enable_classiclink = false  
  instance_tenancy = "default"
  
  tags = {
    Name = "Ecom VPC"
  }
}

resource "aws_subnet" "ecommerce-subnet-public-1" {
  vpc_id     = aws_vpc.ecommerce_vpc.id
  cidr_block = "10.0.1.0/24"
  map_public_ip_on_launch = true
  availability_zone = "us-east-1a"
  tags = {
    Name = "public-subnet-1"
  }
}

resource "aws_subnet" "ecommerce-subnet-public-2" {
  vpc_id     = aws_vpc.ecommerce_vpc.id
  cidr_block = "10.0.2.0/24"
  map_public_ip_on_launch = true
  availability_zone = "us-east-1b"

  tags = {
    Name = "public-subnet-2"
  }
}

resource "aws_subnet" "ecommerce-subnet-private-1" {
  vpc_id     = aws_vpc.ecommerce_vpc.id
  cidr_block = "10.0.3.0/24"
  map_public_ip_on_launch = false
  availability_zone = "us-east-1a"
  

  tags = {
    Name = "private-subnet-1"
  }
}

resource "aws_subnet" "ecommerce-subnet-private-2" {
  vpc_id     = aws_vpc.ecommerce_vpc.id
  cidr_block = "10.0.4.0/24"
  map_public_ip_on_launch = false
  availability_zone = "us-east-1b"

  tags = {
    Name = "private-subnet-2"
  }
  
}

resource "aws_internet_gateway" "ecom_internet_gateway" {
  vpc_id = aws_vpc.ecommerce_vpc.id
  tags = {
    Name = "Ecom IGW"
  }
}

resource "aws_route_table" "rt" {
  vpc_id = aws_vpc.ecommerce_vpc.id
  
  route {
    cidr_block = "0.0.0.0/0"
    gateway_id = aws_internet_gateway.ecom_internet_gateway.id
  }
  
  tags = {
    Name = "EcomVPCRouteTable"
  }
}

resource "aws_route" "route" {
  route_table_id = aws_route_table.rt.id
  destination_cidr_block = "0.0.0.0/0"
  gateway_id = aws_internet_gateway.ecom_internet_gateway.id
}

resource "aws_route_table_association" "rt_association_public_1" {
  subnet_id = aws_subnet.ecommerce-subnet-public-1.id
  route_table_id = aws_route_table.rt.id
}

resource "aws_route_table_association" "rt_association_public_2" {
  subnet_id = aws_subnet.ecommerce-subnet-public-2.id
  route_table_id = aws_route_table.rt.id
}