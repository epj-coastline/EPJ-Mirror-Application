# Coastline

- This repository contains the code for the engineering project Coastline.
- Check out [Coastline Documentation](http://epj.pages.ifs.hsr.ch/2020/coastline/documentation/) to learn more about the project itself.
- Coastline is a distributed application consisting of two main parts:
  - Vue.js single page application referred as «Coastline Client»
  - ASP.NET Core 3 web API referred as «Coastline Server»   

## Repository 

- `client`: contains the code for the Coastline Client
- `server`: contains the code for the Coastline Server
- `k8s`: contains a [Helm Chart](https://helm.sh/) for deploying Coastline to a [Kubernetes](https://kubernetes.io/de/) Cluster 
- `.gitlab-ci.yml`: CI / CD configuration

## Getting started

Clone the repository and follow the instruction in on of the readme files.

- [Readme Client](client/README.md)
- [Readme Server](server/README.md)

## CI / CD 

Visit [CI / CD Pipeline](http://epj.pages.ifs.hsr.ch/2020/coastline/documentation/documentation/design/ci-cd-pipeline/ci-cd-pipeline.html) to learn more about CI / CD for Coastline.

## Deployment

Visit [Deployment](http://epj.pages.ifs.hsr.ch/2020/coastline/documentation/documentation/design/deployment/deployment.html) to learn more about the deployment for Coastline.