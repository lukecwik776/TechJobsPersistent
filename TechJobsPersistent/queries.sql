--Part 1
SELECT COLUMN_NAME, DATA_TYPE 
from INFORMATION_SCHEMA.COLUMNS 
where table_schema = 'techjobs' and table_name = 'jobs'

--Part 2
SELECT name
from employers
where (location = 'St. Louis')

--Part 3

SELECT name, description
FROM skills
INNER JOIN jobskills on skills.Id = jobskills.SkillId;