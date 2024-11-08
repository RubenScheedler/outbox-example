CREATE TABLE IF NOT EXISTS outbox_messages (
     id UUID PRIMARY KEY,
     message_type TEXT NOT NULL,
     content JSONB NOT NULL,
     created_at TIMESTAMP WITH TIME ZONE NOT NULL,
     processed_at TIMESTAMP WITH TIME ZONE NULL,
     error TEXT NULL
);