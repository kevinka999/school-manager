/*
 Navicat Premium Data Transfer

 Source Server         : Local SQL
 Source Server Type    : MySQL
 Source Server Version : 80020
 Source Host           : localhost:3306
 Source Schema         : escola

 Target Server Type    : MySQL
 Target Server Version : 80020
 File Encoding         : 65001

 Date: 25/01/2021 23:33:43
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for aluno
-- ----------------------------
DROP TABLE IF EXISTS `aluno`;
CREATE TABLE `aluno`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Media` float(11, 0) NULL DEFAULT NULL,
  `Ativo` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for alunoprova
-- ----------------------------
DROP TABLE IF EXISTS `alunoprova`;
CREATE TABLE `alunoprova`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `AlunoId` int NOT NULL,
  `ProvaId` int NOT NULL,
  `Ativo` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `fk_alunoid_aluno`(`AlunoId`) USING BTREE,
  INDEX `fk_provaid_prova`(`ProvaId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for gabarito
-- ----------------------------
DROP TABLE IF EXISTS `gabarito`;
CREATE TABLE `gabarito`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ProvaId` int NOT NULL,
  `Pergunta` int NOT NULL,
  `Resposta` char(1) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for prova
-- ----------------------------
DROP TABLE IF EXISTS `prova`;
CREATE TABLE `prova`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `QtdPerguntas` int NULL DEFAULT NULL,
  `Ativo` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for respostaaluno
-- ----------------------------
DROP TABLE IF EXISTS `respostaaluno`;
CREATE TABLE `respostaaluno`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `AlunoProvaId` int NOT NULL,
  `Pergunta` int NOT NULL,
  `Resposta` char(1) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `fk_alunoprovaid_alunoprova`(`AlunoProvaId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
