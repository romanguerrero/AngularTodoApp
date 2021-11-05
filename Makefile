all: start

api/build:
	cd api && dotnet restore

frontend/node_modules: frontend/package.json frontend/yarn.lock
	cd frontend && yarn

node_modules: package.json yarn.lock

.PHONY: frontend
frontend: frontend/node_modules
	cd frontend && yarn start

api: api/build
	cd api && dotnet run

.PHONY: docker-compose
docker-compose:
	docker-compose up

start: node_modules
	./node_modules/.bin/concurrently "make api" "make frontend" "make docker-compose"