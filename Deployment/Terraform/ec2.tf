resource "aws_instance" "ecommerce_machine" {
  ami                         = var.ami
  instance_type               = var.instance_type
  associate_public_ip_address = true
  security_groups             = [aws_security_group.ecommerce_machine_sg.id]
  subnet_id                   = aws_subnet.ecommerce-subnet-public-1.id
  key_name                    = "ec2api"
  iam_instance_profile        = "api"
  
  tags = {
    Name        = var.name
    Environment = var.env
    Provisioner = "Terraform"
    Repo        = var.repo
  }

  provisioner "remote-exec" {
    inline = ["echo 'Wait until SSH is ready to connect'"]

    connection {
      type        = "ssh"
      user        = local.ssh_user
      host        = aws_instance.ecommerce_machine.public_ip
      private_key = file(local.private_key_path)
      agent       = false
    }
  }

  provisioner "local-exec" {
    command = "ansible-playbook -i ${aws_instance.ecommerce_machine.public_ip}, --private-key ${local.private_key_path} ../Ansible/playbook_product_service.yaml"
  }

  provisioner "local-exec" {
    command = "ansible-playbook -i ${aws_instance.ecommerce_machine.public_ip}, --private-key ${local.private_key_path} ../Ansible/playbook_user_service.yaml"
  }
}