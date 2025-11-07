# Build stage
FROM node:20-alpine AS build

WORKDIR /app

# Kopiramo package.json i package-lock.json
COPY package*.json ./

# Instaliramo dependencies
RUN npm install

# Kopiramo ostatak koda
COPY . .

# Build-ujemo aplikaciju
RUN npm run build

# Production stage
FROM nginx:alpine

# Kopiramo build fajlove u nginx
COPY --from=build /app/build /usr/share/nginx/html

# Kopiramo custom nginx config
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
