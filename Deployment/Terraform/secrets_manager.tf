#resource "random_password" "master" {
#  length           = 16
#  special          = true
#  override_special = "_!%^"
#}
#
#resource "aws_secretsmanager_secret" "password" {
#  name = "test-db-password"
#}
#
#resource "aws_secretsmanager_secret_version" "password" {
#  secret_id     = aws_secretsmanager_secret.password.id
#  secret_string = random_password.master.result
#}

#It assumes that there is a Secrets Manager Secret already created
#with the name ecom-database
#data "aws_secretsmanager_secret" "rds-password" {
#  name = "ecom-database"
#}
#
#data "aws_secretsmanager_secret_version" "secret-version" {
#  secret_id = data.aws_secretsmanager_secret.rds-password.id
#}