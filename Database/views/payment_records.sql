SELECT * from payment_records;

CREATE VIEW payment_records as
SELECT
    payment_id,
    payment_method_name "Payment Method",
    username as "User",
    payment_amount,
    payments.createdDate as "Payment Date"
FROM payments
    JOIN payment_methods ON payments.payment_method_id = payment_methods.payment_method_id
    JOIN users ON payments.user_id = users.user_id
GROUP BY payment_id
ORDER BY payment_id DESC;