REM docker-compose up --build -d
REM git pull origin demo
cd .\server-side\WebApiRestful\
dotnet.exe publish -p:PublishProfile=\server\WebApiRestful\Properties\PublishProfiles\FolderProfile.pubxml
 cd ..\..
docker stop app
docker rm app
docker rmi todo:v1.0
docker build -f Dockerfile -t todo:v1.0 .
docker-compose up -d app