local call_key = KEYS[1]
local existing_session_key = KEYS[2]
local new_session_key = KEYS[3]
local new_session = ARGV[1]
local existing_session_json = redis.call('GET', existing_session_key)
local existing_session = cjson.decode(existing_session_json)
existing_session.State = 0  -- Setting State to Inactive (0)
local inactivated_session = cjson.encode(existing_session)
-- TODO: what if those operations will only partially succeed?
redis.call('SET', new_session_key, new_session)
redis.call('SADD', call_key, new_session_key)
redis.call('SET', existing_session_key, inactivated_session)
