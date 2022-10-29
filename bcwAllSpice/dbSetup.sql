-- Active: 1666911632196@@SG-brawny-mantis-3541-6847-mysql-master.servers.mongodirector.com@3306@allspice
CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS recipes(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT 'Primary Key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  title VARCHAR(255) NOT NULL,
  subTitle VARCHAR(255),
  instructions MEDIUMTEXT NOT NULL,
  img MEDIUMTEXT NOT NULL,
  category VARCHAR(255) NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE

)  default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS ingredients(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT 'Primary Key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) NOT NULL,
  quantity VARCHAR(255) NOT NULL,
  recipeId INT NOT NULL,
  FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS favorites(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT 'Primary Key',
  accountId VARCHAR(255) NOT NULL,
  recipeId INT NOT NULL,
  FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

INSERT INTO recipes(title, instructions, img, category, creatorId)
VALUES("Food", "Do stuff", "http://thiscatdoesnotexist.com", "Breakfast", "635c0144746120cc65f3fbb7");

INSERT INTO ingredients(name, quantity, recipeId)
VALUES("sugar", "1 cup", 2);

UPDATE recipes
      SET title = "No longer food", instructions = "Do MORE stuff"
      WHERE id = 2;