-- If the script hasn't changed the session, it returns status 0 and the existing (unchanged) session.
-- Otherwise, it returns status 1 and the created or updated session.
local existing_value = redis.call('GET', KEYS[1])
if existing_value then
    local session = cjson.decode(existing_value)
    if session.State == 1 then
        return { 0, existing_value }
    else
        session.State = 1  -- Setting State to Active (1)
        local updated_value = cjson.encode(session)
        redis.call('SET', KEYS[1], updated_value)
        return { 1, updated_value }
    end
else
    redis.call('SET', KEYS[1], ARGV[1])
    return { 1, ARGV[1] }
end
