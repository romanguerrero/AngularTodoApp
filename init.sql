--  init.sql
create database todo;
use todo;

create user 'todo_rw'@'%' identified by 'todo_pass';
grant all privileges on todo.* to 'todo_rw'@'%';


CREATE TABLE `todos` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `title` varchar(256) NOT NULL,
  `createdDate` DATETIME NOT NULL,
  `done` BOOLEAN NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8;

INSERT INTO `todos` (`title`, `createdDate`, `done`) VALUES ('First Todo', NOW(), false);
INSERT INTO `todos` (`title`, `createdDate`, `done`) VALUES ('Second Todo', NOW(), false);
INSERT INTO `todos` (`title`, `createdDate`, `done`) VALUES ('Third Todo', NOW(), false);
INSERT INTO `todos` (`title`, `createdDate`, `done`) VALUES ('Fourth Todo', NOW(), false);