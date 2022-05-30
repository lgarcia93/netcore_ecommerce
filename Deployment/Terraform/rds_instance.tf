resource "aws_db_instance" "default" {
  allocated_storage = 5
  identifier = "EcommerceDB"
  storage_type = "gp2"
  engine = "mysql"
  engine_version = "5.7"
  instance_class = "db.t2.micro"
  name = "mysql_instance"
  username = "dbadmin"
  password             = data.aws_secretsmanager_secret_version.password
  parameter_group_name = "default.mysql5.7"
  skip_final_snapshot  = false
  publicly_accessible = false   
}

data "aws_secretsmanager_secret" "password" {
  name = "test-db-password"

}

data "aws_secretsmanager_secret_version" "password" {
  secret_id = data.aws_secretsmanager_secret.password
}