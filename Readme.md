1. Created SQL Server container in docker
2. Create .net application
3. Run in container
4. Create docker network
5. Hosted webApp & SQL server on same network
6. Used container name as server name in connection string
7. 2 container running in isolated mode and connected to each other
8. Docker compose for all this steps
9. Will run docker compose and host complete running application



#Pending
1. Auto push new build
2. Run docker compose to use latest build



DB SETUp:
-- Step 1: Create the database
CREATE DATABASE MyAppDb;

-- Step 2: Use the created database
USE MyAppDb;

-- Step 3: Create the table
CREATE TABLE People (
    Id INT,
    Name NVARCHAR(100) NULL,
    Age INT
);

-- Step 4: Insert multiple rows
INSERT INTO People (Id, Name, Age) 
VALUES 
    (1, 'Name 1', 23), 
    (2, 'Name 2', 24);

-- Step 5: Select to verify the data
SELECT * FROM People;




Docker Commands:
docker login -u meet09006 
docker build -t meet09006/webapp:5.0 .
docker push meet09006/webapp:5.0
docker-compose up