# Build stage
FROM node:alpine as build
WORKDIR /usr/src/app
COPY package*.json ./
RUN npm ci
COPY . .
RUN npm run build

# Production stage
FROM node:alpine
WORKDIR /usr/src/app
COPY --from=build /usr/src/app/build ./build
RUN npm install -g serve
EXPOSE 8088
CMD ["serve", "-s", "build", "-l", "4000", "-L"]