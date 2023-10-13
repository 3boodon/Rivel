-- Active: 1690752102154@@127.0.0.1@3306@rivel

-- Roles Table

CREATE TABLE
    roles (
        role_id INT PRIMARY KEY AUTO_INCREMENT,
        role_name VARCHAR(255) NOT NULL,
        createdDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        updatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
    );

-- Users Table

CREATE TABLE
    users (
        user_id INT PRIMARY KEY AUTO_INCREMENT,
        username VARCHAR(255) NOT NULL,
        password VARCHAR(255) NOT NULL,
        role_id INT,
        FOREIGN KEY (role_id) REFERENCES roles(role_id) ON DELETE CASCADE,
        createdDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        updatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
    );

-- Brands Table

CREATE TABLE
    brands (
        brand_id INT PRIMARY KEY AUTO_INCREMENT,
        brand_name VARCHAR(255) NOT NULL,
        createdDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        updatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
    );

-- Models Table

CREATE TABLE
    models (
        model_id INT PRIMARY KEY AUTO_INCREMENT,
        model_name VARCHAR(255) NOT NULL,
        brand_id INT,
        createdDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        updatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
        FOREIGN KEY (brand_id) REFERENCES brands(brand_id) ON DELETE CASCADE ON UPDATE CASCADE
    );

-- Cars Statuses

CREATE TABLE
    car_statuses (
        car_status_id INT PRIMARY KEY AUTO_INCREMENT,
        car_status_name VARCHAR(50) NOT NULL,
        createdDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        updatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
    );

-- Colors Table

CREATE TABLE
    colors (
        color_id INT PRIMARY KEY AUTO_INCREMENT,
        color_name VARCHAR(50) NOT NULL,
        createdDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        updatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
    );

-- Cars Table

CREATE TABLE
    cars (
        car_id INT PRIMARY KEY AUTO_INCREMENT,
        brand_id INT,
        model_id INT,
        color_id INT,
        car_status_id INT,
        price_per_day DECIMAL(10, 2),
        createdDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        updatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
        FOREIGN KEY (brand_id) REFERENCES brands(brand_id) ON DELETE CASCADE ON UPDATE CASCADE,
        FOREIGN KEY (model_id) REFERENCES models(model_id) ON DELETE CASCADE ON UPDATE CASCADE,
        FOREIGN KEY (color_id) REFERENCES colors(color_id) ON DELETE CASCADE ON UPDATE CASCADE,
        FOREIGN KEY (car_status_id) REFERENCES car_statuses(car_status_id) ON DELETE CASCADE ON UPDATE CASCADE
    );

-- warranties table

CREATE TABLE
    warranties (
        warranty_id INT PRIMARY KEY AUTO_INCREMENT,
        warranty_name VARCHAR(255) NOT NULL,
        createdDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        updatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
    );

-- payment_methods table

CREATE TABLE
    payment_methods (
        payment_method_id INT PRIMARY KEY AUTO_INCREMENT,
        payment_method_name VARCHAR(50) NOT NULL,
        createdDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        updatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
    );

-- payments table

CREATE TABLE
    payments (
        payment_id INT PRIMARY KEY AUTO_INCREMENT,
        payment_method_id INT,
        user_id INT,
        payment_amount DECIMAL(10, 2) NOT NULL,
        createdDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        updatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
        FOREIGN KEY (payment_method_id) REFERENCES payment_methods(payment_method_id) ON DELETE CASCADE ON UPDATE CASCADE,
        FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE ON UPDATE CASCADE
    );

--  reservations table

CREATE TABLE
    reservations (
        reservation_id INT PRIMARY KEY AUTO_INCREMENT,
        user_id INT,
        car_id INT,
        pickup_date DATE NOT NULL,
        return_date DATE NOT NULL,
        payment_id INT,
        createdDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        updatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
        FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE ON UPDATE CASCADE,
        FOREIGN KEY (car_id) REFERENCES cars(car_id) ON DELETE CASCADE ON UPDATE CASCADE,
        FOREIGN KEY (payment_id) REFERENCES payments(payment_id) ON DELETE CASCADE ON UPDATE CASCADE
    );

--  rentals table

CREATE TABLE
    rentals (
        rental_id INT PRIMARY KEY AUTO_INCREMENT,
        warranty_id INT,
        user_id INT,
        car_id INT,
        pickup_date DATETIME,
        return_date DATETIME,
        payment_id INT,
        createdDate DATETIME DEFAULT CURRENT_TIMESTAMP,
        updatedDate DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
        FOREIGN KEY (warranty_id) REFERENCES warranties(warranty_id) ON DELETE CASCADE ON UPDATE CASCADE,
        FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE ON UPDATE CASCADE,
        FOREIGN KEY (car_id) REFERENCES cars(car_id) ON DELETE CASCADE ON UPDATE CASCADE,
        FOREIGN KEY (payment_id) REFERENCES payments(payment_id) ON DELETE CASCADE ON UPDATE CASCADE
    );