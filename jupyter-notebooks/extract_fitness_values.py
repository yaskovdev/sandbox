with open("/Users/yaskovdev/Yandex.Disk.localized/gp/gp_robot_evolution_log_2.txt") as file:
    lines = [line.rstrip() for line in file]
    fitness_values = [float(v.split(" ")[5]) for v in lines if v.startswith(";; Best Program Fitness (mean):")]
    print(fitness_values)
