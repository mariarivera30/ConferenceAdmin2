-- MySQL dump 10.13  Distrib 5.6.17, for Win32 (x86)
--
-- Host: 127.0.0.1    Database: conferenceadmin
-- ------------------------------------------------------
-- Server version	5.6.22-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `address`
--

DROP TABLE IF EXISTS `address`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `address` (
  `addressID` bigint(20) NOT NULL AUTO_INCREMENT,
  `line1` varchar(80) DEFAULT NULL,
  `line2` varchar(80) DEFAULT NULL,
  `city` varchar(45) DEFAULT NULL,
  `state` varchar(45) DEFAULT NULL,
  `country` varchar(45) DEFAULT NULL,
  `zipcode` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`addressID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `address`
--

LOCK TABLES `address` WRITE;
/*!40000 ALTER TABLE `address` DISABLE KEYS */;
INSERT INTO `address` VALUES (1,'line1','line2','city','state','country','zipcode');
/*!40000 ALTER TABLE `address` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `administrators`
--

DROP TABLE IF EXISTS `administrators`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `administrators` (
  `administratorsID` int(11) NOT NULL AUTO_INCREMENT,
  `priviledgesID` int(11) NOT NULL,
  `membershipID` bigint(20) NOT NULL,
  `enabled` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`administratorsID`),
  KEY `priviledgeID_idx` (`priviledgesID`),
  KEY `memebershipID_idx` (`membershipID`),
  CONSTRAINT `memebershipID` FOREIGN KEY (`membershipID`) REFERENCES `memberships` (`membershipID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `priviledgeID` FOREIGN KEY (`priviledgesID`) REFERENCES `priviledges` (`priviledgesID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `administrators`
--

LOCK TABLES `administrators` WRITE;
/*!40000 ALTER TABLE `administrators` DISABLE KEYS */;
INSERT INTO `administrators` VALUES (1,1,1,1);
/*!40000 ALTER TABLE `administrators` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `authorizationsubmitted`
--

DROP TABLE IF EXISTS `authorizationsubmitted`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `authorizationsubmitted` (
  `authorizationSubmittedID` int(11) NOT NULL AUTO_INCREMENT,
  `minorID` bigint(20) NOT NULL,
  `documentFile` varchar(2000) NOT NULL,
  `documentName` varchar(45) NOT NULL,
  `creationDate` datetime NOT NULL,
  `deletionDate` datetime DEFAULT NULL,
  PRIMARY KEY (`authorizationSubmittedID`),
  KEY `userID_idx` (`minorID`),
  CONSTRAINT `minorID` FOREIGN KEY (`minorID`) REFERENCES `minors` (`minorsID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `authorizationsubmitted`
--

LOCK TABLES `authorizationsubmitted` WRITE;
/*!40000 ALTER TABLE `authorizationsubmitted` DISABLE KEYS */;
/*!40000 ALTER TABLE `authorizationsubmitted` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `committeeinterface`
--

DROP TABLE IF EXISTS `committeeinterface`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `committeeinterface` (
  `committeID` int(11) NOT NULL AUTO_INCREMENT,
  `firstName` varchar(45) NOT NULL,
  `lastName` varchar(45) NOT NULL,
  `affiliation` varchar(45) NOT NULL,
  `description` varchar(500) NOT NULL,
  PRIMARY KEY (`committeID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `committeeinterface`
--

LOCK TABLES `committeeinterface` WRITE;
/*!40000 ALTER TABLE `committeeinterface` DISABLE KEYS */;
/*!40000 ALTER TABLE `committeeinterface` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `companion`
--

DROP TABLE IF EXISTS `companion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `companion` (
  `companionID` bigint(20) NOT NULL AUTO_INCREMENT,
  `userID` bigint(20) NOT NULL,
  `companionKey` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`companionID`),
  KEY `userID_idx` (`userID`),
  CONSTRAINT `userCompanionID` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `companion`
--

LOCK TABLES `companion` WRITE;
/*!40000 ALTER TABLE `companion` DISABLE KEYS */;
INSERT INTO `companion` VALUES (1,3,'kjjk'),(2,4,'jkjk');
/*!40000 ALTER TABLE `companion` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `companionminor`
--

DROP TABLE IF EXISTS `companionminor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `companionminor` (
  `companionminorID` int(11) NOT NULL AUTO_INCREMENT,
  `minorID` bigint(20) NOT NULL,
  `companionID` bigint(20) NOT NULL,
  PRIMARY KEY (`companionminorID`),
  KEY `minorID_idx` (`minorID`),
  KEY `companionID_idx` (`companionID`),
  CONSTRAINT `companionID` FOREIGN KEY (`companionID`) REFERENCES `companion` (`companionID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `minorcompanionID` FOREIGN KEY (`minorID`) REFERENCES `minors` (`minorsID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `companionminor`
--

LOCK TABLES `companionminor` WRITE;
/*!40000 ALTER TABLE `companionminor` DISABLE KEYS */;
/*!40000 ALTER TABLE `companionminor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `complementarykey`
--

DROP TABLE IF EXISTS `complementarykey`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `complementarykey` (
  `complementarykeyID` bigint(20) NOT NULL AUTO_INCREMENT,
  `sponsorID` bigint(20) NOT NULL,
  `isUsed` tinyint(1) DEFAULT '1',
  `creationDate` datetime NOT NULL,
  `deleitionDate` datetime DEFAULT NULL,
  PRIMARY KEY (`complementarykeyID`),
  KEY `sponsorID_idx` (`sponsorID`),
  CONSTRAINT `sponsorID` FOREIGN KEY (`sponsorID`) REFERENCES `sponsor` (`sponsorID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `complementarykey`
--

LOCK TABLES `complementarykey` WRITE;
/*!40000 ALTER TABLE `complementarykey` DISABLE KEYS */;
/*!40000 ALTER TABLE `complementarykey` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `documentssubmitted`
--

DROP TABLE IF EXISTS `documentssubmitted`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `documentssubmitted` (
  `documentssubmittedID` bigint(20) NOT NULL AUTO_INCREMENT,
  `submissionID` bigint(20) NOT NULL,
  `document` varchar(2000) NOT NULL,
  `creationDate` datetime NOT NULL,
  `deleitionDate` datetime DEFAULT NULL,
  `documentssubmittedcol` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`documentssubmittedID`),
  KEY `submisisonID_idx` (`submissionID`),
  CONSTRAINT `submisisonID` FOREIGN KEY (`submissionID`) REFERENCES `submissions` (`submissionID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='	';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `documentssubmitted`
--

LOCK TABLES `documentssubmitted` WRITE;
/*!40000 ALTER TABLE `documentssubmitted` DISABLE KEYS */;
/*!40000 ALTER TABLE `documentssubmitted` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `evaluationsubmitted`
--

DROP TABLE IF EXISTS `evaluationsubmitted`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `evaluationsubmitted` (
  `evaluationsubmittedID` bigint(20) NOT NULL AUTO_INCREMENT,
  `evaluatiorSubmissionID` bigint(20) NOT NULL,
  `evaluationFile` varchar(2000) NOT NULL,
  `score` int(11) DEFAULT '0',
  `publicFeedback` varchar(1000) DEFAULT NULL,
  `privateFeedback` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`evaluationsubmittedID`),
  KEY `evaluatorsubmissionID_idx` (`evaluatiorSubmissionID`),
  CONSTRAINT `evaluatorsubmissionID` FOREIGN KEY (`evaluatiorSubmissionID`) REFERENCES `evaluatiorsubmission` (`evaluationsubmissionID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `evaluationsubmitted`
--

LOCK TABLES `evaluationsubmitted` WRITE;
/*!40000 ALTER TABLE `evaluationsubmitted` DISABLE KEYS */;
/*!40000 ALTER TABLE `evaluationsubmitted` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `evaluatiorsubmission`
--

DROP TABLE IF EXISTS `evaluatiorsubmission`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `evaluatiorsubmission` (
  `evaluationsubmissionID` bigint(20) NOT NULL,
  `evaluatorID` bigint(20) NOT NULL,
  `submissionID` bigint(20) NOT NULL,
  `statusEvaluation` varchar(45) DEFAULT NULL,
  `creationDate` datetime NOT NULL,
  `deleitionDate` datetime DEFAULT NULL,
  PRIMARY KEY (`evaluationsubmissionID`),
  KEY `evaluatorID_idx` (`evaluatorID`),
  KEY `submissionID_idx` (`submissionID`),
  CONSTRAINT `evaluatorID` FOREIGN KEY (`evaluatorID`) REFERENCES `evaluators` (`evaluatorsID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `submissionID` FOREIGN KEY (`submissionID`) REFERENCES `submissions` (`submissionID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `evaluatiorsubmission`
--

LOCK TABLES `evaluatiorsubmission` WRITE;
/*!40000 ALTER TABLE `evaluatiorsubmission` DISABLE KEYS */;
/*!40000 ALTER TABLE `evaluatiorsubmission` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `evaluators`
--

DROP TABLE IF EXISTS `evaluators`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `evaluators` (
  `evaluatorsID` bigint(20) NOT NULL AUTO_INCREMENT,
  `userID` bigint(20) NOT NULL,
  PRIMARY KEY (`evaluatorsID`),
  KEY `userID_idx` (`userID`),
  CONSTRAINT `userEvaluatorID` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `evaluators`
--

LOCK TABLES `evaluators` WRITE;
/*!40000 ALTER TABLE `evaluators` DISABLE KEYS */;
/*!40000 ALTER TABLE `evaluators` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `interfaceinformation`
--

DROP TABLE IF EXISTS `interfaceinformation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `interfaceinformation` (
  `interfaceID` int(11) NOT NULL AUTO_INCREMENT,
  `attribute` varchar(45) NOT NULL,
  `content` varchar(1000) NOT NULL,
  PRIMARY KEY (`interfaceID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `interfaceinformation`
--

LOCK TABLES `interfaceinformation` WRITE;
/*!40000 ALTER TABLE `interfaceinformation` DISABLE KEYS */;
/*!40000 ALTER TABLE `interfaceinformation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `memberships`
--

DROP TABLE IF EXISTS `memberships`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `memberships` (
  `membershipID` bigint(20) NOT NULL AUTO_INCREMENT,
  `membershipTypeID` int(11) NOT NULL,
  `email` varchar(45) NOT NULL,
  `password` varchar(45) NOT NULL,
  `creationDate` datetime DEFAULT NULL,
  `deletionDate` datetime DEFAULT NULL,
  `emailConfirmation` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`membershipID`),
  UNIQUE KEY `email_UNIQUE` (`email`),
  KEY `memebershipTypeID_idx` (`membershipTypeID`),
  CONSTRAINT `memebershipTypeID` FOREIGN KEY (`membershipTypeID`) REFERENCES `membershiptype` (`membershipTypeID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `memberships`
--

LOCK TABLES `memberships` WRITE;
/*!40000 ALTER TABLE `memberships` DISABLE KEYS */;
INSERT INTO `memberships` VALUES (1,1,'maria.rivera30@gmail.com','maria','0001-01-01 00:00:00',NULL,0),(2,2,'juan.rivera@gmail.com','juan','0001-01-01 00:00:00',NULL,0),(10,1,'dsdfd','sfgsfdgfd','0001-01-01 00:00:00',NULL,0),(11,1,'juana','puebla','0001-01-01 00:00:00',NULL,0),(12,2,'evaliu','sdsd','0001-01-01 00:00:00',NULL,0);
/*!40000 ALTER TABLE `memberships` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `membershiptype`
--

DROP TABLE IF EXISTS `membershiptype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `membershiptype` (
  `membershipTypeID` int(11) NOT NULL AUTO_INCREMENT,
  `membershipTypeName` varchar(45) NOT NULL,
  PRIMARY KEY (`membershipTypeID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `membershiptype`
--

LOCK TABLES `membershiptype` WRITE;
/*!40000 ALTER TABLE `membershiptype` DISABLE KEYS */;
INSERT INTO `membershiptype` VALUES (1,'Admin'),(2,'User');
/*!40000 ALTER TABLE `membershiptype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `minors`
--

DROP TABLE IF EXISTS `minors`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `minors` (
  `minorsID` bigint(20) NOT NULL AUTO_INCREMENT,
  `userID` bigint(20) NOT NULL,
  `authorizationStatus` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`minorsID`),
  KEY `userID_idx` (`userID`),
  CONSTRAINT `userID` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `minors`
--

LOCK TABLES `minors` WRITE;
/*!40000 ALTER TABLE `minors` DISABLE KEYS */;
INSERT INTO `minors` VALUES (1,1,0);
/*!40000 ALTER TABLE `minors` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `panels`
--

DROP TABLE IF EXISTS `panels`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `panels` (
  `panelsID` bigint(20) NOT NULL AUTO_INCREMENT,
  `submissionID` bigint(20) NOT NULL,
  `panelistNames` varchar(500) NOT NULL,
  `plan` varchar(500) DEFAULT NULL,
  `guideQuestion` varchar(1000) DEFAULT NULL,
  `formatDescription` varchar(500) DEFAULT NULL,
  `necessaryEquipment` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`panelsID`),
  KEY `submissionID_idx` (`submissionID`),
  CONSTRAINT `submissionPanelID` FOREIGN KEY (`submissionID`) REFERENCES `submissions` (`submissionID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `panels`
--

LOCK TABLES `panels` WRITE;
/*!40000 ALTER TABLE `panels` DISABLE KEYS */;
/*!40000 ALTER TABLE `panels` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payment`
--

DROP TABLE IF EXISTS `payment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `payment` (
  `paymentID` bigint(20) NOT NULL AUTO_INCREMENT,
  `paymentTypeID` int(11) NOT NULL,
  `creationDate` datetime DEFAULT NULL,
  `deletionDate` datetime DEFAULT NULL,
  PRIMARY KEY (`paymentID`),
  KEY `paymentTypeID_idx` (`paymentTypeID`),
  CONSTRAINT `paymentTypeID` FOREIGN KEY (`paymentTypeID`) REFERENCES `paymenttype` (`paymentTypeID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payment`
--

LOCK TABLES `payment` WRITE;
/*!40000 ALTER TABLE `payment` DISABLE KEYS */;
INSERT INTO `payment` VALUES (1,1,NULL,NULL);
/*!40000 ALTER TABLE `payment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `paymentbill`
--

DROP TABLE IF EXISTS `paymentbill`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `paymentbill` (
  `paymentBillID` bigint(20) NOT NULL AUTO_INCREMENT,
  `paymentID` bigint(20) DEFAULT NULL,
  `transactionid` varchar(45) NOT NULL,
  `AmountPaid` int(11) NOT NULL,
  `methodOfPayment` varchar(45) NOT NULL,
  `creditCardNumber` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`paymentBillID`),
  KEY `paymentID_idx` (`paymentID`),
  CONSTRAINT `paymentID` FOREIGN KEY (`paymentID`) REFERENCES `payment` (`paymentID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `paymentbill`
--

LOCK TABLES `paymentbill` WRITE;
/*!40000 ALTER TABLE `paymentbill` DISABLE KEYS */;
/*!40000 ALTER TABLE `paymentbill` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `paymentcomplementary`
--

DROP TABLE IF EXISTS `paymentcomplementary`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `paymentcomplementary` (
  `paymentcomplementaryID` bigint(20) NOT NULL AUTO_INCREMENT,
  `paymentID` bigint(20) NOT NULL,
  `complementaryKeyID` bigint(20) NOT NULL,
  PRIMARY KEY (`paymentcomplementaryID`),
  KEY `paymentID_idx` (`paymentID`),
  CONSTRAINT `complementaryKeyID` FOREIGN KEY (`paymentcomplementaryID`) REFERENCES `complementarykey` (`complementarykeyID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `paymentComplementaryID` FOREIGN KEY (`paymentID`) REFERENCES `payment` (`paymentID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `paymentcomplementary`
--

LOCK TABLES `paymentcomplementary` WRITE;
/*!40000 ALTER TABLE `paymentcomplementary` DISABLE KEYS */;
/*!40000 ALTER TABLE `paymentcomplementary` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `paymenttype`
--

DROP TABLE IF EXISTS `paymenttype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `paymenttype` (
  `paymentTypeID` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  PRIMARY KEY (`paymentTypeID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `paymenttype`
--

LOCK TABLES `paymenttype` WRITE;
/*!40000 ALTER TABLE `paymenttype` DISABLE KEYS */;
INSERT INTO `paymenttype` VALUES (1,'Bill');
/*!40000 ALTER TABLE `paymenttype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `priviledges`
--

DROP TABLE IF EXISTS `priviledges`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `priviledges` (
  `priviledgesID` int(11) NOT NULL AUTO_INCREMENT,
  `priviledgestType` varchar(45) NOT NULL,
  PRIMARY KEY (`priviledgesID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `priviledges`
--

LOCK TABLES `priviledges` WRITE;
/*!40000 ALTER TABLE `priviledges` DISABLE KEYS */;
INSERT INTO `priviledges` VALUES (1,'Admin'),(2,'Finance'),(3,'CommitteEvaluator'),(4,'Evaluator');
/*!40000 ALTER TABLE `priviledges` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `registration`
--

DROP TABLE IF EXISTS `registration`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `registration` (
  `registrationID` bigint(20) NOT NULL AUTO_INCREMENT,
  `userID` bigint(20) NOT NULL,
  `paymentID` bigint(20) NOT NULL,
  `date1` tinyint(1) DEFAULT '0',
  `date2` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`registrationID`),
  KEY `userID_idx` (`userID`),
  KEY `paymentID_idx` (`paymentID`),
  CONSTRAINT `paymentIDRegistration` FOREIGN KEY (`paymentID`) REFERENCES `payment` (`paymentID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `userIDRegister` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `registration`
--

LOCK TABLES `registration` WRITE;
/*!40000 ALTER TABLE `registration` DISABLE KEYS */;
/*!40000 ALTER TABLE `registration` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sponsor`
--

DROP TABLE IF EXISTS `sponsor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sponsor` (
  `sponsorID` bigint(20) NOT NULL AUTO_INCREMENT,
  `sponsorType` int(11) NOT NULL,
  `addressID` bigint(20) NOT NULL,
  `paymentID` bigint(20) DEFAULT NULL,
  `firstName` varchar(45) NOT NULL,
  `lastName` varchar(45) NOT NULL,
  `title` varchar(45) DEFAULT NULL,
  `company` varchar(45) NOT NULL,
  `phone` varchar(45) DEFAULT NULL,
  `email` varchar(45) DEFAULT NULL,
  `logo` varchar(45) DEFAULT NULL,
  `creationDate` datetime DEFAULT NULL,
  `deleitionDate` datetime DEFAULT NULL,
  PRIMARY KEY (`sponsorID`),
  KEY `sponsorTypeID_idx` (`sponsorType`),
  KEY `addressID_idx` (`addressID`),
  KEY `paymentID_idx` (`paymentID`),
  CONSTRAINT `SponsoraddressID` FOREIGN KEY (`addressID`) REFERENCES `address` (`addressID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `payment` FOREIGN KEY (`paymentID`) REFERENCES `payment` (`paymentID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `sponsorTypeID` FOREIGN KEY (`sponsorType`) REFERENCES `sponsortype` (`sponsortypeID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8 COMMENT='			';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sponsor`
--

LOCK TABLES `sponsor` WRITE;
/*!40000 ALTER TABLE `sponsor` DISABLE KEYS */;
INSERT INTO `sponsor` VALUES (1,1,1,NULL,'gdf','hgfhdfg','hfgdh','gfdhgfdhg','hgfd','gfdhgfd',NULL,NULL,NULL),(2,1,1,NULL,'Angel','De La Torre','Student','UPRM','7876075141','ANGEL@UPR.ED',NULL,NULL,NULL),(3,1,1,NULL,'ttt','tttt','ttt','ttt','ttt','tt',NULL,NULL,NULL),(4,1,1,NULL,'ttttttttttttttttttttttttttttttttttttttttt','fdf','dfdf','dfd','t','t',NULL,NULL,NULL),(5,1,1,NULL,'dfsff','dfd','dfdf','dfd','dfdfd','dfdf',NULL,NULL,NULL),(6,1,1,NULL,'vcvc','vcv','cvcvc','vc','cvcvc','vcv',NULL,NULL,NULL),(7,1,1,NULL,'yyy','yyy','yyyy','yyyy','yyy','yyy',NULL,NULL,NULL),(8,1,1,NULL,'ui','ui','ui','u','i','uii',NULL,NULL,NULL),(9,1,1,NULL,'chumba Catala','j','j','j','j','j',NULL,NULL,NULL),(10,1,1,NULL,'amorr','k','k','k','k','k',NULL,NULL,NULL),(11,1,1,NULL,'WWWW','WWWW','WW','WW','WWW','WW',NULL,NULL,NULL),(12,1,1,NULL,'jai','k','k','k','k','k',NULL,NULL,NULL),(13,1,1,NULL,'ghg','ghg','ghg','gh','gh','gh',NULL,NULL,NULL),(14,1,1,NULL,'juana','k','k','k','k','k',NULL,NULL,NULL),(15,1,1,NULL,'ty','ty','ty','ty','y','ty',NULL,NULL,NULL),(16,1,1,NULL,'jkj','jk','j','k','k','lk',NULL,NULL,NULL),(17,1,1,NULL,'kik','k','k','k','k','k',NULL,NULL,NULL),(18,1,1,NULL,'heidi','i','i','i','i','i',NULL,NULL,NULL),(19,1,1,NULL,'kjkjk','kjk','j','kj','jk','jk',NULL,NULL,NULL),(20,1,1,NULL,'yuyuyu','uy','uy','u','yu','y',NULL,NULL,NULL),(21,1,1,NULL,'fg','fg','fg','fg','fg','fg',NULL,NULL,NULL),(22,1,1,NULL,'hjhj','jh','j','hj','h','jh',NULL,NULL,NULL),(23,1,1,NULL,'jkjk','jk','jk','jk','kj','kj',NULL,NULL,NULL),(24,1,1,NULL,'holaaaa','jk','jk','jk','','',NULL,NULL,NULL),(25,1,1,NULL,'alejo','h','h','h','h','h',NULL,NULL,NULL),(26,1,1,NULL,'huhuh','h','h','h','h','h',NULL,NULL,NULL),(27,1,1,NULL,'hj','hj','h','jh','j','hj',NULL,NULL,NULL),(28,1,1,NULL,'lolaL','L','L','L','L','L',NULL,NULL,NULL);
/*!40000 ALTER TABLE `sponsor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sponsortype`
--

DROP TABLE IF EXISTS `sponsortype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sponsortype` (
  `sponsortypeID` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `amount` varchar(45) DEFAULT NULL,
  `benefit1` varchar(45) DEFAULT NULL,
  `benefit2` varchar(45) DEFAULT NULL,
  `benefit3` varchar(45) DEFAULT NULL,
  `benefit4` varchar(45) DEFAULT NULL,
  `benefit5` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`sponsortypeID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sponsortype`
--

LOCK TABLES `sponsortype` WRITE;
/*!40000 ALTER TABLE `sponsortype` DISABLE KEYS */;
INSERT INTO `sponsortype` VALUES (1,'Gold','555','',NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `sponsortype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `submissions`
--

DROP TABLE IF EXISTS `submissions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `submissions` (
  `submissionID` bigint(20) NOT NULL AUTO_INCREMENT,
  `userID` bigint(20) NOT NULL,
  `topicID` int(11) NOT NULL,
  `submissionTypeID` int(11) NOT NULL,
  `hasApplied` tinyint(1) DEFAULT '0',
  `title` varchar(45) DEFAULT NULL,
  `status` tinyint(1) DEFAULT NULL,
  `creationDate` datetime NOT NULL,
  `deleitionDate` datetime DEFAULT NULL,
  PRIMARY KEY (`submissionID`),
  KEY `submissionTypeID_idx` (`submissionTypeID`),
  KEY `topicID_idx` (`topicID`),
  KEY `userID_idx` (`userID`),
  CONSTRAINT `submissionType` FOREIGN KEY (`submissionTypeID`) REFERENCES `submissiontype` (`submissiontypeID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `topicCategoryID` FOREIGN KEY (`topicID`) REFERENCES `topiccategory` (`topiccategoryID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `userIDSumission` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `submissions`
--

LOCK TABLES `submissions` WRITE;
/*!40000 ALTER TABLE `submissions` DISABLE KEYS */;
/*!40000 ALTER TABLE `submissions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `submissiontype`
--

DROP TABLE IF EXISTS `submissiontype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `submissiontype` (
  `submissiontypeID` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `description` varchar(500) NOT NULL,
  PRIMARY KEY (`submissiontypeID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `submissiontype`
--

LOCK TABLES `submissiontype` WRITE;
/*!40000 ALTER TABLE `submissiontype` DISABLE KEYS */;
/*!40000 ALTER TABLE `submissiontype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `templates`
--

DROP TABLE IF EXISTS `templates`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `templates` (
  `templateID` bigint(20) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `document` varchar(2000) NOT NULL,
  PRIMARY KEY (`templateID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `templates`
--

LOCK TABLES `templates` WRITE;
/*!40000 ALTER TABLE `templates` DISABLE KEYS */;
/*!40000 ALTER TABLE `templates` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `templatesubmission`
--

DROP TABLE IF EXISTS `templatesubmission`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `templatesubmission` (
  `templatesubmissionID` int(11) NOT NULL AUTO_INCREMENT,
  `templateID` bigint(20) NOT NULL,
  `submissionID` bigint(20) NOT NULL,
  `creationDate` datetime NOT NULL,
  `deleitionDate` datetime DEFAULT NULL,
  PRIMARY KEY (`templatesubmissionID`),
  KEY `submissionID_idx` (`submissionID`),
  KEY `templateID_idx` (`templateID`),
  CONSTRAINT `submissionTemplateID` FOREIGN KEY (`submissionID`) REFERENCES `submissions` (`submissionID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `templateAssigniedID` FOREIGN KEY (`templateID`) REFERENCES `templates` (`templateID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `templatesubmission`
--

LOCK TABLES `templatesubmission` WRITE;
/*!40000 ALTER TABLE `templatesubmission` DISABLE KEYS */;
/*!40000 ALTER TABLE `templatesubmission` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `topiccategory`
--

DROP TABLE IF EXISTS `topiccategory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `topiccategory` (
  `topiccategoryID` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  PRIMARY KEY (`topiccategoryID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `topiccategory`
--

LOCK TABLES `topiccategory` WRITE;
/*!40000 ALTER TABLE `topiccategory` DISABLE KEYS */;
/*!40000 ALTER TABLE `topiccategory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `userID` bigint(20) NOT NULL AUTO_INCREMENT,
  `membershipID` bigint(20) NOT NULL,
  `userTypeID` int(11) NOT NULL,
  `firstName` varchar(45) NOT NULL,
  `lastName` varchar(45) NOT NULL,
  `title` varchar(45) DEFAULT NULL,
  `affiliationName` varchar(45) DEFAULT NULL,
  `phone` varchar(45) DEFAULT NULL,
  `addressID` bigint(20) NOT NULL,
  `userFax` varchar(45) DEFAULT NULL,
  `registrationStatus` tinyint(1) DEFAULT '0',
  `hasApplied` tinyint(1) DEFAULT '0',
  `acceptanceStatus` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`userID`),
  KEY `addressID_idx` (`addressID`),
  KEY `usertypeID_idx` (`userTypeID`),
  KEY `membershipID_idx` (`membershipID`),
  CONSTRAINT `addressIDuser` FOREIGN KEY (`addressID`) REFERENCES `address` (`addressID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `membershipID` FOREIGN KEY (`membershipID`) REFERENCES `memberships` (`membershipID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `userTypeID` FOREIGN KEY (`userTypeID`) REFERENCES `usertype` (`userTypeID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8 COMMENT='	';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,2,1,'juan','rivera','dsf','hg','45454',1,'543',0,1,1),(2,2,6,'ty','ty','t','yt','yt',1,'y',0,1,1),(3,10,6,'jijki','jkjk','kjj','jkj','kjk',1,'j',1,0,0),(4,11,5,'rtrrtrt','yt','y','t','tyt',1,NULL,1,0,1),(5,12,7,'uyyu','yuyu','uyu','uyu','yuyu',1,'yu',0,0,0);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usertype`
--

DROP TABLE IF EXISTS `usertype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `usertype` (
  `userTypeID` int(11) NOT NULL AUTO_INCREMENT,
  `userTypeName` varchar(45) NOT NULL,
  `description` varchar(500) DEFAULT NULL,
  `registrationCost` int(11) DEFAULT NULL,
  `registrationLateFee` int(11) DEFAULT NULL,
  PRIMARY KEY (`userTypeID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usertype`
--

LOCK TABLES `usertype` WRITE;
/*!40000 ALTER TABLE `usertype` DISABLE KEYS */;
INSERT INTO `usertype` VALUES (1,'High School Student','wew',7,10),(2,'Undergraduate Student','wew',5,5),(3,'Graduate Student','ew',5,5),(4,'Professional Industry','wew',5,5),(5,'Professional Academia','eww',5,5),(6,'Companion','kjk',6,6),(7,'Evaluator','df',6,6);
/*!40000 ALTER TABLE `usertype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `workshops`
--

DROP TABLE IF EXISTS `workshops`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `workshops` (
  `workshopID` int(11) NOT NULL AUTO_INCREMENT,
  `submissionID` bigint(20) NOT NULL,
  `duration` varchar(45) DEFAULT NULL,
  `delivery` varchar(45) DEFAULT NULL,
  `plan` varchar(45) DEFAULT NULL,
  `necessary equipment` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`workshopID`),
  KEY `submissionID_idx` (`submissionID`),
  CONSTRAINT `submissionWorkshopID` FOREIGN KEY (`submissionID`) REFERENCES `submissions` (`submissionID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `workshops`
--

LOCK TABLES `workshops` WRITE;
/*!40000 ALTER TABLE `workshops` DISABLE KEYS */;
/*!40000 ALTER TABLE `workshops` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2015-03-15  2:06:29
