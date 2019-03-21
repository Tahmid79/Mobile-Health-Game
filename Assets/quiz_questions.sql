BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS `questions` (
	`question_text`	TEXT,
	`question_id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`option_one_text`	TEXT,
	`option_two_text`	TEXT,
	`option_three_text`	TEXT,
	`option_four_text`	TEXT,
	`correct_option`	INTEGER NOT NULL DEFAULT 0
);
INSERT INTO `questions` VALUES ('',1,NULL,NULL,NULL,NULL,0);
INSERT INTO `questions` VALUES ('What system is the brain part of?',2,'Skeletal','Nervous',NULL,NULL,0);
INSERT INTO `questions` VALUES ('What system is the brain part of?',3,'Skeletal','Nervous','Circulatory','Endocrine',1);
INSERT INTO `questions` VALUES ('What is the general cause of seizures?',4,'Poor diet','Lack of exercise','Erratic brain activity','Sodium deficiency',2);
INSERT INTO `questions` VALUES ('What is another name for Grand Mal Seizure?',5,'Focal Seizure','Tonic-Clonic Seizure','Atonic Seizure','Abdominal Epilepsy',1);
COMMIT;
