if [ -f /tmp/latest_artifact_api/env_vars ]; then
  source /tmp/latest_artifact_api/env_vars
else
  echo "env_vars file not found!"
  exit 1
fi

sed -i "s|{{IMAGE_NAME}}|$ECR_REPOSITORY_URI/$ECR_REPOSITORY_API:latest|g" "/tmp/latest_artifact_api/k8s/openglotApi-deployment.yaml"
kubectl apply -f /tmp/latest_artifact_api/k8s/ --namespace openglot
