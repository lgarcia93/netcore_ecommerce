#resource "aws_lambda_function" "lambda_authorizer" {
#  function_name = "API Gateway Authorizer"
#  role          = aws_iam_role.lambda_authorizer_role.arn
#}
#
#resource "aws_iam_role" "lambda_authorizer_role" {
#  assume_role_policy = <<EOF
#    {
#      "Version": "2012-10-17",
#      "Statement": [
#        {
#          "Action": "sts:AssumeRole",
#          "Principal": {
#            "Service": "lambda.amazonaws.com"
#          },
#          "Effect": "Allow",
#          "Sid": ""
#        }
#      ]
#    }
#    EOF
#  
#  name = "LambdaAPIGatewayAuthorizerRole"
#}