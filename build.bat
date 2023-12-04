cd server-side
docker build -f WebApiRestful/Dockerfile -t todo:v1.0 .
cd ..
docker-compose up --build -d