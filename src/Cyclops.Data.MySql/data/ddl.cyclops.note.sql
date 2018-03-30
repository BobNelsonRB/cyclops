CREATE TABLE `note` (
  `NoteId` int(11) NOT NULL AUTO_INCREMENT,
  `Id` varchar(36) NOT NULL,
  `Display` varchar(100) NOT NULL,
  `Body` varchar(500) NOT NULL,
  `Disposition` varchar(25) NOT NULL,
  `Tags` varchar(150) NOT NULL,
  `CreatedBy` varchar(25) NOT NULL,
  `ModifiedBy` varchar(25) NOT NULL,
  `CreatedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`NoteId`),
  UNIQUE KEY `Id_UNIQUE` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;