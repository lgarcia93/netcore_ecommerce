resource "aws_dynamodb_table" "dynamo_products_table" {
  name           = "products"
  billing_mode   = "PROVISIONED"
  read_capacity  = 20
  write_capacity = 20
  hash_key       = "ProductID"
    
  attribute {
    name = "ProductId"
    type = "S"
  }  
}