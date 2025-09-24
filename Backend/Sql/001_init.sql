-- Initial schema for Landscape Management System (Dapper/PostgreSQL)

CREATE TABLE IF NOT EXISTS landscapes (
    id BIGSERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    description TEXT NULL,
    created_at TIMESTAMPTZ NOT NULL DEFAULT now()
);

-- Seed example (optional)
-- INSERT INTO landscapes (name, description) VALUES
--   ('North Campus', 'Primary grounds'),
--   ('South Park', 'Recreation area');

