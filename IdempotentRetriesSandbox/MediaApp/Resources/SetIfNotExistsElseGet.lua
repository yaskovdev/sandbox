local existing_value = redis.call('GET', KEYS[1])
if existing_value then
    return existing_value
else
    redis.call('SET', KEYS[1], ARGV[1])
    return nil
end
