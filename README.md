# MachineStreamBackend

## How To Deploy

1. Install a clickhouse server and make the admin password as 'root' .
2. Create the table with the SQL command in clickhouse_create_table.sql by clickhouse-client
3. Package the project by VS2022 as docker image
4. Run the image as container instance by docker
