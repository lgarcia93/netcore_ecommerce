resource "aws_db_instance" "default" {
  allocated_storage = 5
  identifier        = "ecommerce-db"
  storage_type      = "gp2"
  engine            = "mysql"
  engine_version    = "5.7"
  instance_class    = "db.t2.micro"
  db_name           = "mysql_instance"
  username          = "dbadmin"
  #password             = data.aws_secretsmanager_secret_version.secret-version.secret_string
  password             = "dev12345"
  parameter_group_name = "default.mysql5.7"
  skip_final_snapshot  = true
  publicly_accessible  = false
}