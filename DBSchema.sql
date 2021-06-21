-- DROP AND RECREATE DATABASE IF ALREADY EXIST

DROP DATABASE secretgarden;
CREATE DATABASE secretgarden;
USE secretgarden;

-- DB STRUCTURE (DDL)

CREATE TABLE customer(
	customer_id int(8) NOT NULL AUTO_INCREMENT,
	first_name varchar(64) NOT NULL,
	last_name varchar(64) NOT NULL,
	address varchar(128) NOT NULL,
	telephone varchar(11) NOT NULL,
	establish_date date NOT NULL,
	premium_register_date date,
	premium_end_date date,
	PRIMARY KEY (customer_id)
);

CREATE TABLE customer_order(
	order_id int(8) NOT NULL AUTO_INCREMENT,
	customer_id int(8) NOT NULL,
	order_datetime datetime NOT NULL,
	prepare_datetime datetime NOT NULL,
	is_delivery int(1) NOT NULL DEFAULT 0,
	completed int(1) NOT NULL DEFAULT 0,
	admin_id int(8) NOT NULL,
	payment_method varchar(128),
	paid int(1) NOT NULL DEFAULT 0,
	PRIMARY KEY (order_id)
);

CREATE TABLE order_item(
	order_id int(8) NOT NULL,
	item_id int(8) NOT NULL,
	quantity int(3) NOT NULL DEFAULT 1,
	customization TEXT,
	PRIMARY KEY (order_id,item_id)
);

CREATE TABLE item(
	item_id int(8) NOT NULL AUTO_INCREMENT,
	item_name varchar(64) NOT NULL,
	price decimal(6,2) NOT NULL,
	item_type varchar(64) NOT NULL,
	cake_size int(2),
	PRIMARY KEY (item_id)
);

CREATE TABLE admin_account(
	admin_id int(8) NOT NULL AUTO_INCREMENT,
	first_name varchar(64) NOT NULL,
	last_name varchar(64) NOT NULL,
	password_salt varchar(5) NOT NULL,
	password_hash varchar(64) NOT NULL,
	establish_date date NOT NULL,
	PRIMARY KEY (admin_id)
);

-- DB DUMMY DATA (DML)

INSERT INTO item VALUES
(1, "Bagel", 3.49, 1, NULL),
(2, "Biscotti", 5.80, 1, NULL),
(3, "Boule", 8.69, 1, NULL),
(4, "Breadstick", 3.25, 1, NULL),
(5, "Brioche", 8.25, 1, NULL),
(6, "Coppia Ferrarese", 6.10, 1, NULL),
(7, "English muffin", 3.00, 1, NULL),
(8, "Kifli", 5.00, 1, NULL),
(9, "Melonpan", 5.20, 1, NULL),
(10, "Rice bread", 9.59, 1, NULL),
(11, "Rieska", 4.99, 1, NULL),
(12, "Baumkuchen", 99.99, 1, NULL),
(13, "Butter Cake", 6.50, 2, 4),
(14, "Sponge Cake", 4.50, 2, 4),
(15, "Genoise Cake", 6.50, 2, 4),
(16, "Angel Food Cake", 6.50, 2, 4),
(17, "Biscuit Cake", 6.50, 2, 4),
(18, "Chiffon Cake", 6.50, 2, 4),
(19, "Baked Flourless Cake", 6.50, 2, 4),
(20, "Unbaked Flourless Cake", 6.50, 2, 4),
(21, "Carrot Cake", 6.50, 2, 4),
(22, "Red Velvet Cake", 6.50, 2, 4);
