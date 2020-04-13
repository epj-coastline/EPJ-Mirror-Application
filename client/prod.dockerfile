FROM node:12 as build-stage
WORKDIR /app
COPY package*.json ./
RUN npm ci
COPY ./ .
RUN npm run build

FROM nginx:1.17-alpine as production-stage
RUN mkdir /app
COPY --from=build-stage /app/dist /app
COPY nginx.conf /etc/nginx/nginx.conf
COPY entrypoint.sh /
RUN ["chmod", "+x", "entrypoint.sh"]
EXPOSE 80

CMD ["/entrypoint.sh"]
