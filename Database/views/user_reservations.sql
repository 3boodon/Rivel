SELECT * from user_reservations;

CREATE VIEW
    user_reservations as
SELECT
    CONCAT(
        color_name,
        ' ',
        brand_name,
        ' - ',
        model_name
    ) as "reserved_car",
    pickup_date,
    return_date,
    payment_method_name as "payment_method",
    payments.payment_amount as "payment_amount",
    reservations.createdDate as "reserved_on"
FROM available_cars
    JOIN reservations on available_cars.car_id = reservations.car_id
    JOIN payments on payments.payment_id = reservations.payment_id
    JOIN payment_methods on payment_methods.payment_method_id = payments.payment_method_id
    JOIN users on reservations.user_id = 9 -- this Id should be dynamically changed depending on the logged in user
GROUP BY reservation_id
ORDER BY `reserved_on` DESC;

drop view user_reservations;