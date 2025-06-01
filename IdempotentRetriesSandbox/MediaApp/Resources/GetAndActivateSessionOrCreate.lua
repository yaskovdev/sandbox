-- 0 created
-- 1 activated
-- 2 unchanged
local existing_value = redis.call('GET', KEYS[1])
if existing_value then
    local session = cjson.decode(existing_value)
    if session.State == 1 then
        return 2
    else
        session.State = 1  -- Setting State to Active (1)
        local updated_value = cjson.encode(session)
        redis.call('SET', KEYS[1], updated_value)
        return 1
    end
else
    redis.call('SET', KEYS[1], ARGV[1])
    return 0
end
