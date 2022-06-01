data "aws_iam_policy_document" "default" {
  statement {
    actions = ["sts:AssumeRole"]

    principals {
      type        = "Service"
      identifiers = ["ec2.amazonaws.com"]
    }
  }
}

resource "aws_iam_role" "default" {
  name               = "ec2_role"
  assume_role_policy = data.aws_iam_policy_document.default.json
}

resource "aws_iam_role_policy_attachment" "rpa_ec2containerservice" {
  role       = aws_iam_role.default.name
  policy_arn = "arn:aws:iam::aws:policy/service-role/AmazonEC2ContainerServiceforEC2Role"
}

resource "aws_iam_role_policy" "dynamodb-policy" {
  name = "dynamodb_policy"
  role = aws_iam_role.default.id
  policy = jsonencode({
    "Version" : "2012-10-17",
    "Statement" : [
      {
        "Effect" : "Allow",
        "Action" : ["dynamodb:*"],
        "Resource" : "*"
      }
    ]
  })
}

resource "aws_iam_role_policy" "cloud_map_policity" {
  name = "cloudmap_policy"
  role = aws_iam_role.default.id

  policy = jsonencode({
    "Version" : "2012-10-17",
    "Statement" : [
      {
        "Sid" : "AllowInstancePermissions",
        "Effect" : "Allow",
        "Action" : [
          "servicediscovery:RegisterInstance",
          "servicediscovery:DeregisterInstance",
          "servicediscovery:DiscoverInstances",
          "servicediscovery:Get*",
          "servicediscovery:List*",
          "route53:GetHostedZone",
          "route53:ListHostedZonesByName",
          "route53:ChangeResourceRecordSets",
          "route53:CreateHealthCheck",
          "route53:GetHealthCheck",
          "route53:DeleteHealthCheck",
          "route53:UpdateHealthCheck",
          "ec2:DescribeInstances"
        ],
        "Resource" : "*"
      }
    ]
  })

}

resource "aws_iam_instance_profile" "ec2_instance_profile" {
  name = "Ecommerce-EC2-Instance-Profile"
  role = aws_iam_role.default.name
}