resource "aws_dynamodb_table" "dynamo_products_table" {
  name           = "products"
  billing_mode   = "PROVISIONED"
  read_capacity  = 20
  write_capacity = 20
  hash_key       = "ProductId"

  attribute {
    name = "ProductId"
    type = "S"
  }
}

resource "aws_dynamodb_table" "dynamo_users_table" {
  name           = "users"
  billing_mode   = "PROVISIONED"
  read_capacity  = 2
  write_capacity = 2
  hash_key       = "UserId"

  attribute {
    name = "UserId"
    type = "S"
  }

  attribute {
    name = "UserName"
    type = "S"
  }

  global_secondary_index {
    hash_key        = "UserName"
    name            = "UserNameIndex"
    projection_type = "ALL"

    write_capacity = 1
    read_capacity  = 1
  }
}