-- define the next query as a VIEW

-- this view will be used to show the available cars

select * from available_cars;

create view available_cars as
select
    car_id,
    brand_name,
    model_name,
    color_name,
    car_status_name,
    price_per_day
from cars
    join brands on brands.brand_id = cars.brand_id
    join models on models.model_id = cars.model_id
    join colors on colors.color_id = cars.color_id
    join car_statuses on car_statuses.car_status_id = cars.car_status_id
where
    car_status_name = 'available'
order by car_id;

select * from available_cars where color_name = 'red';