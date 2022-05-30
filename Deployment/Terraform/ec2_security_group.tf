resource "aws_security_group" "ecommerce_machine_sg" {
  name   = "sg_ecommerce"
  vpc_id = aws_vpc.ecommerce_vpc.id

  ingress {
    from_port   = 22
    to_port     = 22
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    from_port = 5000
    protocol  = "tcp"
    to_port   = 5000
    #Accept requests on port 80 only from Network Load Balancer
#    cidr_blocks = [
#      aws_subnet.ecommerce-subnet-private-1.cidr_block,
#      aws_subnet.ecommerce-subnet-private-2.cidr_block
#    ]
    #cidr_blocks = formatlist("%s/32", data.aws_network_interface.lb.*.private_ip)
    security_groups = [
      aws_security_group.alb_security_group.id
    ]
    #cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    from_port = 6000
    protocol  = "tcp"
    to_port   = 6000
#    cidr_blocks = [
#      aws_subnet.ecommerce-subnet-private-1.cidr_block,
#      aws_subnet.ecommerce-subnet-private-2.cidr_block
#    ]
    #Accept requests on port 80 only from Network Load Balancer    
    #cidr_blocks = formatlist("%s/32", data.aws_network_interface.lb.*.private_ip)
    #cidr_blocks = ["0.0.0.0/0"]
    security_groups = [
      aws_security_group.alb_security_group.id
    ]
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}

data "aws_network_interface" "lb" {

  count = length([aws_subnet.ecommerce-subnet-private-1, aws_subnet.ecommerce-subnet-private-2])

  filter {
    name   = "description"
    values = ["ELB ${aws_lb.network_ecommerce_loadbalancer.arn_suffix}"]
  }
  filter {
    name   = "subnet-id"
    values = [[aws_subnet.ecommerce-subnet-private-1.id, aws_subnet.ecommerce-subnet-private-2.id][count.index]]
  }
}