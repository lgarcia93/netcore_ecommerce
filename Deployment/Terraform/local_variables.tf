locals {
  ssh_user         = "ec2-user"
  private_key_path = "~/.ssh/ec2api.pem"
  account_id       = data.aws_caller_identity.current.account_id
}