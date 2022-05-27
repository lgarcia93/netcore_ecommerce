#resource "aws_apigatewayv2_authorizer" "ecomapi_authorizer" {
#  api_id          = aws_apigatewayv2_api.ecommerceapi.id
#  authorizer_type = "REQUEST"
#  name            = "eom-authorizer"
#  
#  authorizer_uri = aws_lambda_function.lambda_authorizer.invoke_arn
#}