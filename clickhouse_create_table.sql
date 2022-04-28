create table Event (Id String, Machine_id String, Status String, Timestamp datetime) 
engine=MergeTree() 
order by (Machine_id,Id,Timestamp) 
primary key (Machine_id,Id)