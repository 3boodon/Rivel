CREATE VIEW all_reservations As
SELECT
    username as "user",
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
    payments.createdDate as "payment_date"
FROM available_cars
    JOIN reservations on available_cars.car_id = reservations.car_id
    JOIN payments on payments.payment_id = reservations.payment_id
    JOIN users on users.user_id = reservations.user_id
    JOIN payment_methods on payment_methods.payment_method_id = payments.payment_method_id
GROUP BY reservation_id
ORDER BY `payment_date` DESC;

SELECT reserved_car from all_reservations;

drop view all_reservations;