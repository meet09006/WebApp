# Completed tasks:
1. Created SQL Server container in docker
2. Create .net application
3. Run in container
4. Create docker network
5. Hosted webApp & SQL server on same network
6. Used container name as server name in connection string
7. 2 container running in isolated mode and connected to each other
8. Docker compose for all this steps
9. Run docker compose and host complete running application
10. Integrated New Relic


# Pending
1. Auto push new build
2. Run docker compose to use latest build



# DB SETUp:
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




# Docker Commands (Replace username meet09006, with your username):

Docker Login: docker login -u meet09006 

Docker build: docker build -t meet09006/webapp:5.0 .

Docker push to Hub: docker push meet09006/webapp:5.0

Run Docker compose: docker-compose up
