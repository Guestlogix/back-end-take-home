--This file contains general sql queries executed during the development of this system.
select * from airport
select * from route
select air.name, ori.iata_three as ori_iata_three, des.iata_three as des_iata_three, ori.latitude as ori_latitude, ori.longitude as ori_longitude, des.latitude as des_latitude, des.longitude as des_longitude
from route rou
  inner join airline air on air.id = rou.airline_id
  inner join airport ori on ori.id = rou.origin_airport_id
  inner join airport des on des.id = rou.destination_airport_id  