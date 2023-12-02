cd SERVER
docker build -f SuntacAPI/Dockerfile -t suntac:v1.0 .
cd ..
docker-compose up --build -d