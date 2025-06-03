local call_exists = redis.call('EXISTS', KEYS[1])
if call_exists == 1 then
    return 0
else
    redis.call('SET', KEYS[2], ARGV[1])
    redis.call('SADD', KEYS[1], KEYS[2])
    return 1
end
