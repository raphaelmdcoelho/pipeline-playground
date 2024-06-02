-- Enable UUID extension if not already enabled
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Create Category table and insert data
CREATE TABLE IF NOT EXISTS category (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    description VARCHAR(255) NOT NULL,
    is_active BOOLEAN NOT NULL
);

INSERT INTO category (id, description, is_active)
VALUES
(uuid_generate_v4(), 'Category 1', true),
(uuid_generate_v4(), 'Category 2', false);

-- Create User table and insert data
CREATE TABLE IF NOT EXISTS app_user (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    category_id UUID REFERENCES category(id),
    name VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL
);

INSERT INTO app_user (id, category_id, name, email, password)
VALUES
(uuid_generate_v4(), (SELECT id FROM category LIMIT 1), 'User 1', 'user1@example.com', 'password1'),
(uuid_generate_v4(), (SELECT id FROM category OFFSET 1 LIMIT 1), 'User 2', 'user2@example.com', 'password2');
