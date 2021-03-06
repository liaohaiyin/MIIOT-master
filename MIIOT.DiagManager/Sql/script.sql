/*
 Navicat Premium Data Transfer

 Source Server         : mysql
 Source Server Type    : MySQL
 Source Server Version : 50725
 Source Host           : localhost:3306
 Source Schema         : mit

 Target Server Type    : MySQL
 Target Server Version : 50725
 File Encoding         : 65001

 Date: 23/11/2020 18:16:04
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for cargodetail
-- ----------------------------
DROP TABLE IF EXISTS `cargodetail`;
CREATE TABLE `cargodetail`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `CID` int(11) NULL DEFAULT NULL,
  `ProductNo` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ProductName` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ProductType` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FactoryName` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Unit` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `OutputNum` int(11) NULL DEFAULT NULL,
  `CheckNum` int(11) NULL DEFAULT NULL,
  `Price` int(11) NULL DEFAULT NULL,
  `BatchNo` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreateDate` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `VerifyDate` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of cargodetail
-- ----------------------------
INSERT INTO `cargodetail` VALUES (1, 1, 'GYSSJGG001200', '氢氧化钠溶液168', '100ML/袋', '升东药店', '袋', 2, 2, 80, 'A001888', '2019-11-02', '2019-12-02');
INSERT INTO `cargodetail` VALUES (2, 1, '32004R', '氯霉素试剂9', '(500mL×4瓶/箱)', '广湖国药', '袋', 1, 6, 90, 'A001', '2015-11-02', '2019-11-02');

-- ----------------------------
-- Table structure for cargoinfo
-- ----------------------------
DROP TABLE IF EXISTS `cargoinfo`;
CREATE TABLE `cargoinfo`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `SortNo` int(11) NULL DEFAULT NULL,
  `CargoName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `SupplierName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `OutputName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Operator` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of cargoinfo
-- ----------------------------
INSERT INTO `cargoinfo` VALUES (1, 1, 'YS20200305001', '广佛国药', '冷库', '李1');
INSERT INTO `cargoinfo` VALUES (2, 2, 'YS20200305008', '动佛国药', '冷库888', 'GG');

-- ----------------------------
-- Table structure for menuinfo
-- ----------------------------
DROP TABLE IF EXISTS `menuinfo`;
CREATE TABLE `menuinfo`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ParentID` int(11) NULL DEFAULT NULL,
  `SortNo` int(11) NULL DEFAULT NULL,
  `Name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Path` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Param` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Icon` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `PermissionInfoCode` varchar(36) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Remark` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 8 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of menuinfo
-- ----------------------------
INSERT INTO `menuinfo` VALUES (1, -1, 1, '验收管理', '/MIIOT.DiagManager;Component/Pages/WorkAcceptInfoPage.xaml', NULL, 'Icon-Home', NULL, NULL);
INSERT INTO `menuinfo` VALUES (3, -1, 2, '申领管理', '/MIIOT.DiagManager;Component/Pages/ApplyBudgetPage.xaml', NULL, 'Icon-Record', NULL, NULL);
INSERT INTO `menuinfo` VALUES (4, -1, 3, '退库管理', '/MIIOT.DiagManager;Component/Pages/FallbackLibraryPage.xaml', NULL, 'Icon-Money', NULL, NULL);
INSERT INTO `menuinfo` VALUES (5, -1, 4, '退货管理', '/MIIOT.DiagManager;Component/Pages/FallBackCargoPage.xaml', NULL, 'Icon-Visitor', NULL, NULL);
INSERT INTO `menuinfo` VALUES (6, -1, 5, '重打标签', '/MIIOT.DiagManager;Component/Pages/PrintLabelPage.xaml', NULL, 'Icon-Report', NULL, NULL);
INSERT INTO `menuinfo` VALUES (7, -1, 6, '领用', '/MIIOT.DiagManager;Component/Pages/CommMenuPage.xaml', NULL, 'Icon-Save', NULL, NULL);

-- ----------------------------
-- Table structure for moduleinfo
-- ----------------------------
DROP TABLE IF EXISTS `moduleinfo`;
CREATE TABLE `moduleinfo`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `AssemblyString` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `SystemType` int(11) NULL DEFAULT NULL,
  `Remark` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of moduleinfo
-- ----------------------------

-- ----------------------------
-- Table structure for productinfo
-- ----------------------------
DROP TABLE IF EXISTS `productinfo`;
CREATE TABLE `productinfo`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `BID` int(11) NULL DEFAULT NULL,
  `ProductNo` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ProductName` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ProductType` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FactoryName` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Unit` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `VerifyNum` int(11) NULL DEFAULT NULL,
  `CheckNum` int(11) NULL DEFAULT NULL,
  `Price` double NULL DEFAULT NULL,
  `BatchNo` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreateDate` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `VerifyDate` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of productinfo
-- ----------------------------
INSERT INTO `productinfo` VALUES (1, 1, '2.55.04.32004R', '氯霉素试剂', 'BC5380/5390/M-53LH/(500mL×4瓶/箱)', '广州国药', '盒', 100, 100, 100, '2019112600', '2019-12-02', '2020-12-02');
INSERT INTO `productinfo` VALUES (2, 1, 'GYSSJGG001200', '氢氧化钠溶液', '100ML/袋', '广州国药', '袋', 9, 10, 10, '2019112655', '2019-12-02', '2019-12-08');

-- ----------------------------
-- Table structure for pub_accept
-- ----------------------------
DROP TABLE IF EXISTS `pub_accept`;
CREATE TABLE `pub_accept`  (
  `id` bigint(20) NOT NULL COMMENT 'id',
  `organ_id` bigint(20) NOT NULL COMMENT '机构ID',
  `source_type` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=送货单',
  `source_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '来源单ID',
  `source_no` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '来源单号',
  `origin_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '原始单ID',
  `supply_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '供应商ID',
  `supply_name` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '供应商名称',
  `supply_time` datetime(0) NULL DEFAULT NULL COMMENT '送货时间',
  `storehouse_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '收货库房',
  `accept_person_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '验收人ID',
  `accept_person_name` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '验收人',
  `check_person_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '复核人ID',
  `check_person_name` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '复核人',
  `status` tinyint(3) NOT NULL DEFAULT 0 COMMENT '0=初始 1=待验收 2=验收确认 3=复核确认 99=拒收',
  `is_delete` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=正常 1=删除',
  `creater_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '创建人id',
  `gmt_create` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `modifier_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '修改人id',
  `gmt_modified` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE INDEX `source_no_index`(`source_no`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '验收单' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_accept
-- ----------------------------
INSERT INTO `pub_accept` VALUES (1273141198527799298, 1, 0, 1273141198527799298, 'PO2020061700008', 'PO2020061700008', 32, '广西慧篆商贸有限公司', '2020-06-17 14:31:24', 34, 0, '', 0, '', 3, 0, 0, '2020-06-17 14:31:30', 0, '2020-06-17 14:32:25');
INSERT INTO `pub_accept` VALUES (1273145056507338754, 1, 0, 1273145056507338754, 'PO2020061700009', 'PO2020061700009', 35, '广西真世好生物科技', '2020-06-17 14:46:37', 34, 0, '', 0, '', 3, 0, 0, '2020-06-17 14:46:50', 0, '2020-06-17 14:47:02');
INSERT INTO `pub_accept` VALUES (1273147782796550145, 1, 0, 1273147782796550145, 'PO2020061700010', 'PO2020061700010', 36, '广西信禾通医疗投资有限公司', '2020-06-17 14:57:23', 34, 0, '', 0, '', 3, 0, 0, '2020-06-17 14:57:40', 0, '2020-06-17 15:31:03');
INSERT INTO `pub_accept` VALUES (1273155039835848705, 1, 0, 1273155039835848705, 'PO2020061700011', 'PO2020061700011', 36, '广西信禾通医疗投资有限公司', '2020-06-17 15:08:16', 34, 0, '', 0, '', 2, 0, 0, '2020-06-17 15:26:30', 0, '2020-07-13 16:53:19');
INSERT INTO `pub_accept` VALUES (1273155040234307585, 1, 0, 1273155040234307585, 'PO2020061700012', 'PO2020061700012', 42, '广州市伟之晨科仪器械有限公司', '2020-06-17 15:13:21', 34, 0, '', 0, '', 3, 0, 0, '2020-06-17 15:26:30', 0, '2020-06-17 16:29:34');
INSERT INTO `pub_accept` VALUES (1273160869029216258, 1, 0, 1273160869029216258, 'PO2020061700013', 'PO2020061700013', 36, '广西信禾通医疗投资有限公司', '2020-06-17 15:49:28', 34, 0, '', 0, '', 3, 0, 0, '2020-06-17 15:49:40', 0, '2020-06-17 16:29:22');
INSERT INTO `pub_accept` VALUES (1273184650749673474, 1, 0, 1273184650749673474, 'PO2020061700014', 'PO2020061700014', 37, '南宁乾美商贸有限公司', '2020-06-17 17:24:04', 34, 0, '', 0, '', 3, 0, 0, '2020-06-17 17:24:10', 0, '2020-06-17 17:24:44');
INSERT INTO `pub_accept` VALUES (1273499013859639297, 1, 0, 1273499013859639297, 'PO2020061800001', 'PO2020061800001', 35, '广西真世好生物科技', '2020-06-18 14:13:10', 34, 0, '', 0, '', 0, 0, 0, '2020-06-18 14:13:20', 0, '2020-06-18 14:13:20');

-- ----------------------------
-- Table structure for pub_accept_dtl
-- ----------------------------
DROP TABLE IF EXISTS `pub_accept_dtl`;
CREATE TABLE `pub_accept_dtl`  (
  `id` bigint(20) NOT NULL COMMENT 'id',
  `organ_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '机构ID',
  `accept_id` bigint(20) NOT NULL COMMENT '验收表ID',
  `source_type` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=送货单',
  `source_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '来源单ID',
  `source_dtl_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '来源明细ID',
  `source_no` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '来源单号',
  `origin_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '原始单ID',
  `origin_dtl_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '原始明细ID',
  `goods_id` bigint(20) NOT NULL COMMENT '商品ID',
  `goods_no` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '商品编码',
  `goods_name` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '商品名称',
  `goods_spec` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '商品规格',
  `goods_factory_name` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '生产厂家',
  `goods_unit` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '单位',
  `delivery_qty` int(11) NOT NULL DEFAULT 0 COMMENT '送货数量',
  `check_qty` int(11) NOT NULL DEFAULT 0 COMMENT '复核数量',
  `lot_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '批号ID',
  `lot_no` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '批号',
  `batch_id` bigint(20) NOT NULL COMMENT '批次',
  `price` double(16, 4) NOT NULL DEFAULT 0.0000 COMMENT '单价',
  `pprod_date` date NULL DEFAULT NULL COMMENT '生产日期',
  `pvalid_date` date NULL DEFAULT NULL COMMENT '有效日期至',
  `status` tinyint(3) NOT NULL DEFAULT 0 COMMENT '0=初始 1=待验收 2=验收确认 3=复核确认 99=拒收',
  `is_delete` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=正常 1=删除',
  `creater_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '创建人id',
  `gmt_create` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `modifier_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '修改人id',
  `gmt_modified` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  `batch_no` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '批次编码',
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE INDEX `accept_dtl_index`(`organ_id`, `source_type`, `source_dtl_id`) USING BTREE COMMENT '验收细单组合索引',
  INDEX `source_no_index`(`source_no`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '验收明细' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_accept_dtl
-- ----------------------------
INSERT INTO `pub_accept_dtl` VALUES (1273141198565548034, 1, 1273141198527799298, 0, 1273141198527799298, 1273141198565548034, 'PO2020061700008', 'PO2020061700008', '82c011e5-ef0a-44f9-ac0e-c1c76e5e6892', 348, 'G202006001', '肌酸激酶MB同工酶测定试剂盒-CK-MB', '7060     R1 2*80ml   R2 2*20ml', '北京利德曼生化股份有限公司', '盒', 2, 0, 1273141198414553090, '0617-010', 1273141198578130946, 220.0000, '2019-10-14', '2021-01-18', 3, 0, 0, '2020-06-17 14:31:30', 0, '2020-06-17 14:32:25', '202006170009');
INSERT INTO `pub_accept_dtl` VALUES (1273145056511533058, 1, 1273145056507338754, 0, 1273145056507338754, 1273145056511533058, 'PO2020061700009', 'PO2020061700009', '8a8ebb89-c907-4cba-b76f-04866e756a72', 348, 'G202006001', '肌酸激酶MB同工酶测定试剂盒-CK-MB', '7060     R1 2*80ml   R2 2*20ml', '北京利德曼生化股份有限公司', '盒', 1, 0, 1273145056473784321, '0617-011', 1273145056515727361, 220.0000, '2019-12-24', '2021-03-24', 3, 0, 0, '2020-06-17 14:46:50', 0, '2020-06-17 14:47:02', '202006170010');
INSERT INTO `pub_accept_dtl` VALUES (1273147782800744449, 1, 1273147782796550145, 0, 1273147782796550145, 1273147782800744449, 'PO2020061700010', 'PO2020061700010', '1e73b7de-2b14-406b-9a96-4bf9404cfe42', 350, 'G202006003', '纤维连接蛋白检测试剂盒', 'R1:45ML*2', '重庆中元生物技术有限公司', '盒', 3, 0, 1273147782771384322, '0617-12', 1273147782804938754, 600.0000, '2019-08-13', '2021-01-21', 3, 0, 0, '2020-06-17 14:57:40', 0, '2020-06-17 15:31:03', '202006170011');
INSERT INTO `pub_accept_dtl` VALUES (1273155039877791746, 1, 1273155039835848705, 0, 1273155039835848705, 1273155039877791746, 'PO2020061700011', 'PO2020061700011', 'f58ad8dc-8d41-4477-be6b-2c216dcc22dc', 348, 'G202006001', '肌酸激酶MB同工酶测定试剂盒-CK-MB', '7060     R1 2*80ml   R2 2*20ml', '北京利德曼生化股份有限公司', '盒', 3, 0, 1273155039689048065, '0617-13', 1273155039902957570, 220.0000, '2019-10-21', '2021-04-13', 2, 0, 0, '2020-06-17 15:26:30', 0, '2020-07-13 16:53:19', '202006170012');
INSERT INTO `pub_accept_dtl` VALUES (1273155040242696193, 1, 1273155040234307585, 0, 1273155040234307585, 1273155040242696193, 'PO2020061700012', 'PO2020061700012', '76e2f2f9-f185-4fb9-9fdf-c5ceed9c1b80', 352, 'G20200615', 'γ-谷氨酰氨基转移酶测定试剂盒', 'R1: 4×40mL+R2: 4×40mL；', '贝克曼库尔特实验系统(苏州)有限公司', '盒', 3, 0, 1273155040196558849, '0617-015', 1273155040251084802, 1000.0000, '2019-12-09', '2020-12-16', 3, 0, 0, '2020-06-17 15:26:30', 0, '2020-06-17 16:29:34', '202006170013');
INSERT INTO `pub_accept_dtl` VALUES (1273160869033410561, 1, 1273160869029216258, 0, 1273160869029216258, 1273160869033410561, 'PO2020061700013', 'PO2020061700013', '482f2bc8-4186-428e-9588-163f63ff3899', 348, 'G202006001', '肌酸激酶MB同工酶测定试剂盒-CK-MB', '7060     R1 2*80ml   R2 2*20ml', '北京利德曼生化股份有限公司', '盒', 1, 0, 1273160868995661825, '0617-016', 1273160869037604866, 220.0000, '2020-01-14', '2021-02-17', 3, 0, 0, '2020-06-17 15:49:40', 0, '2020-06-17 16:29:22', '202006170014');
INSERT INTO `pub_accept_dtl` VALUES (1273184650766450689, 1, 1273184650749673474, 0, 1273184650749673474, 1273184650766450689, 'PO2020061700014', 'PO2020061700014', 'c88f315c-fb64-4380-829f-508f372da86f', 348, 'G202006001', '肌酸激酶MB同工酶测定试剂盒-CK-MB', '7060     R1 2*80ml   R2 2*20ml', '北京利德曼生化股份有限公司', '盒', 2, 0, 1273184650732896258, '0617-020', 1273184650774839298, 220.0000, '2019-12-16', '2020-12-17', 3, 0, 0, '2020-06-17 17:24:10', 0, '2020-06-17 17:24:44', '202006170015');
INSERT INTO `pub_accept_dtl` VALUES (1273499013868027906, 1, 1273499013859639297, 0, 1273499013859639297, 1273499013868027906, 'PO2020061800001', 'PO2020061800001', 'a31f963d-f441-4431-82b7-13660d14794d', 350, 'G202006003', '纤维连接蛋白检测试剂盒', 'R1:45ML*2', '重庆中元生物技术有限公司', '盒', 3, 0, 1273499013817696258, '0618-001', 1273499013872222210, 600.0000, '2019-11-06', '2021-06-23', 0, 0, 0, '2020-06-18 14:13:20', 0, '2020-06-18 14:13:20', '202006180000');

-- ----------------------------
-- Table structure for pub_apply
-- ----------------------------
DROP TABLE IF EXISTS `pub_apply`;
CREATE TABLE `pub_apply`  (
  `id` bigint(20) NOT NULL COMMENT 'id',
  `organ_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '机构ID',
  `source_type` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=中台 1=消耗申领',
  `source_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '来源单ID',
  `source_no` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '来源单号',
  `origin_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '原始单ID',
  `depm_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '申领科室ID',
  `depm_name` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '申领科室',
  `apply_storehouse_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '申领库房ID',
  `apply_storehouse_name` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '申领库房',
  `storehouse_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '出库库房ID',
  `storehouse_name` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '出库库房',
  `apply_name` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '申领人',
  `apply_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '申领时间',
  `check_person_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '复核人ID',
  `check_person_name` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '复核人姓名',
  `check_time` datetime(0) NULL DEFAULT NULL COMMENT '复核时间',
  `status` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=初始 1=拣选 2=复核完成 99=作废',
  `is_delete` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=正常 1=删除',
  `creater_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '创建人id',
  `gmt_create` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `modifier_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '修改人id',
  `gmt_modified` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '申领' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_apply
-- ----------------------------
INSERT INTO `pub_apply` VALUES (1273142284642820098, 1, 1, 0, 'WB00000000034', '1273142284642820098', '1271260329185861633', '冷藏库一', 37, '冰箱一', 34, '中心库', 'admin', '2020-06-17 14:35:49', 1, 'admin', '2020-06-17 14:35:49', 2, 0, 1, '2020-06-17 14:35:49', 1, '2020-06-17 14:35:49');
INSERT INTO `pub_apply` VALUES (1273142967777501186, 1, 0, 0, 'WA2020061700007', '8eeee459-ad9f-47a0-aa2f-3718217444b6', '0000000003', '冷藏库一', 34, '中心库', 37, '冰箱一', 'admin', '2020-06-17 14:38:28', 1, 'admin', '2020-06-17 14:40:05', 2, 0, 0, '2020-06-17 14:38:32', 0, '2020-06-17 14:38:32');
INSERT INTO `pub_apply` VALUES (1273144724985356290, 1, 1, 0, 'WB00000000035', '1273144724985356290', '', '', 0, '', 34, '中心库', 'admin', '2020-06-17 14:45:31', 1, 'admin', '2020-06-17 14:45:31', 2, 0, 1, '2020-06-17 14:45:31', 1, '2020-06-17 14:45:31');
INSERT INTO `pub_apply` VALUES (1273145160748376065, 1, 1, 0, 'WB00000000036', '1273145160748376065', '', '', 0, '', 34, '中心库', 'admin', '2020-06-17 14:47:15', 1, 'admin', '2020-06-17 14:47:15', 2, 0, 1, '2020-06-17 14:47:15', 1, '2020-06-17 14:47:15');
INSERT INTO `pub_apply` VALUES (1273158964836171777, 1, 1, 0, 'WB00000000037', '1273158964836171777', '', '', 0, '', 34, '中心库', 'admin', '2020-06-17 15:42:06', 1, 'admin', '2020-06-17 15:42:06', 2, 0, 1, '2020-06-17 15:42:06', 1, '2020-06-17 15:42:06');
INSERT INTO `pub_apply` VALUES (1273159137905737729, 1, 1, 0, 'WB00000000038', '1273159137905737729', '1271260371124707329', '冷藏库二', 36, '冰箱二', 34, '中心库', 'admin', '2020-06-17 15:42:47', 1, 'admin', '2020-06-17 15:42:47', 2, 0, 1, '2020-06-17 15:42:47', 1, '2020-06-17 15:42:47');
INSERT INTO `pub_apply` VALUES (1273195941811847169, 1, 0, 0, 'WA2020061700011', '934ce474-55b2-4fbc-b83b-636252c1bd1b', '0000000003', '冷藏库一', 34, '中心库', 37, '冰箱一', 'admin', '2020-06-17 18:08:55', 1, 'admin', '2020-06-17 18:10:03', 2, 0, 0, '2020-06-17 18:09:02', 0, '2020-06-17 18:09:02');
INSERT INTO `pub_apply` VALUES (1273199130623737858, 1, 1, 0, 'WB00000000039', '1273199130623737858', '1271260329185861633', '冷藏库一', 37, '冰箱一', 34, '中心库', 'admin', '2020-06-17 18:21:42', 1, 'admin', '2020-06-17 18:21:42', 2, 0, 1, '2020-06-17 18:21:42', 1, '2020-06-17 18:21:42');
INSERT INTO `pub_apply` VALUES (1278578827633229825, 1, 0, 0, 'WA2020070200000', '0673f0e8-e645-4b57-a9c8-833acf63b0fb', '0000000002', '检验组', 34, '中心库', 35, '检验库房', 'admin', '2020-07-02 14:38:34', 0, '', NULL, 1, 0, 0, '2020-07-02 14:38:42', 0, '2020-07-02 14:38:42');

-- ----------------------------
-- Table structure for pub_apply_back
-- ----------------------------
DROP TABLE IF EXISTS `pub_apply_back`;
CREATE TABLE `pub_apply_back`  (
  `id` bigint(20) NOT NULL COMMENT 'id',
  `organ_id` bigint(20) NOT NULL COMMENT '机构ID',
  `source_type` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=中台 1=手工',
  `source_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '来源单ID',
  `source_no` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '来源单号',
  `origin_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '原始单ID',
  `depm_id` bigint(20) NULL DEFAULT NULL COMMENT '申退科室ID',
  `depm_name` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '申退科室',
  `source_storehouse_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '申退库房ID',
  `source_storehouse_name` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '申退库房',
  `target_storehouse_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '退回库房ID',
  `target_storehouse_name` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '退回库房',
  `check_person_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '复核人ID',
  `check_person_name` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '复核人',
  `status` tinyint(3) NOT NULL DEFAULT 0 COMMENT '0=初始 1=已复核 99=作废',
  `is_delete` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=正常 1=删除',
  `creater_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '创建人id',
  `gmt_create` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `modifier_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '修改人id',
  `gmt_modified` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE INDEX `source_no_index`(`source_no`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '退库单' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_apply_back
-- ----------------------------
INSERT INTO `pub_apply_back` VALUES (1273144517371502594, 1, 1, 0, 'AB00000000057', '', 1271260329185861633, '冷藏库一', 37, '冰箱一', 34, '中心库', 0, '', 1, 0, 1, '2020-06-17 14:44:41', 1, '2020-06-17 14:44:41');
INSERT INTO `pub_apply_back` VALUES (1273159335239352321, 1, 1, 0, 'AB00000000059', '', 1271260371124707329, '冷藏库二', 36, '冰箱二', 34, '中心库', 0, '', 1, 0, 1, '2020-06-17 15:43:34', 1, '2020-06-17 15:43:34');

-- ----------------------------
-- Table structure for pub_apply_back_dtl
-- ----------------------------
DROP TABLE IF EXISTS `pub_apply_back_dtl`;
CREATE TABLE `pub_apply_back_dtl`  (
  `id` bigint(20) NOT NULL COMMENT 'id',
  `organ_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '机构ID',
  `apply_back_id` bigint(20) NOT NULL COMMENT '退货表ID',
  `source_type` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=中台 1=手工',
  `source_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '来源单ID',
  `source_dtl_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '来源明细ID',
  `source_no` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '来源单号',
  `origin_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '原始单ID',
  `origin_dtl_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '原始明细ID',
  `goods_id` bigint(20) NOT NULL COMMENT '商品ID',
  `goods_no` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '商品编码',
  `goods_name` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '商品名称',
  `goods_spec` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '商品规格',
  `goods_factory_name` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '生产厂家',
  `goods_unit` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '单位',
  `back_qty` int(11) NOT NULL COMMENT '退货数量',
  `check_qty` int(11) NOT NULL DEFAULT 0 COMMENT '复核数量',
  `status` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=初始 1=已复核 99=作废',
  `is_delete` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=正常 1=删除',
  `creater_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '创建人id',
  `gmt_create` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `modifier_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '修改人id',
  `gmt_modified` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  `lot_no` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '批号',
  `batch_no` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '批次',
  `singlecode` varchar(1024) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '单品码',
  `back_reason` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '原因',
  `pprod_date` date NULL DEFAULT NULL COMMENT '生产日期',
  `pvalid_date` date NULL DEFAULT NULL COMMENT '有效日期至',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `source_no_index`(`source_no`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '退库明细表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_apply_back_dtl
-- ----------------------------
INSERT INTO `pub_apply_back_dtl` VALUES (1273144517430222849, 1, 1273144517371502594, 1, 0, 0, 'AB00000000057', '', '', 348, 'G202006001', '肌酸激酶MB同工酶测定试剂盒-CK-MB', '7060     R1 2*80ml   R2 2*20ml', '北京利德曼生化股份有限公司', '盒', 1, 0, 1, 0, 1, '2020-06-17 14:44:41', 0, '2020-11-10 14:05:33', NULL, NULL, NULL, '正常退库', '2020-08-25', '2021-08-25');
INSERT INTO `pub_apply_back_dtl` VALUES (1273159335298072578, 1, 1273159335239352321, 1, 0, 0, 'AB00000000059', '', '', 350, 'G202006003', '纤维连接蛋白检测试剂盒', 'R1:45ML*2', '重庆中元生物技术有限公司', '盒', 1, 0, 1, 0, 1, '2020-06-17 15:43:34', 0, '2020-11-10 14:05:36', NULL, NULL, NULL, '正常退库', '2020-08-25', '2023-08-25');

-- ----------------------------
-- Table structure for pub_apply_dtl
-- ----------------------------
DROP TABLE IF EXISTS `pub_apply_dtl`;
CREATE TABLE `pub_apply_dtl`  (
  `id` bigint(20) NOT NULL COMMENT 'id',
  `organ_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '机构ID',
  `apply_id` bigint(20) NOT NULL COMMENT '申领表ID',
  `source_type` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=正常申领 1=消耗申领',
  `source_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '来源单ID',
  `source_dtl_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '来源明细ID',
  `origin_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '原始单ID',
  `origin_dtl_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '原始明细单ID',
  `goods_id` bigint(20) NOT NULL COMMENT '商品ID',
  `goods_no` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '商品编码',
  `goods_name` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '商品名称',
  `goods_spec` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '商品规格',
  `goods_factory_name` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '生产厂家',
  `goods_unit` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '单位',
  `price` double(16, 4) NOT NULL DEFAULT 0.0000 COMMENT '单价',
  `apply_qty` int(11) NOT NULL DEFAULT 0 COMMENT '申领数量',
  `check_qty` int(11) NOT NULL DEFAULT 0 COMMENT '复核数量',
  `status` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=初始 1=拣选 2=复核完成 99=作废',
  `is_delete` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=正常 1=删除',
  `creater_id` bigint(20) NULL DEFAULT 0 COMMENT '创建人id',
  `gmt_create` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `modifier_id` bigint(20) NULL DEFAULT 0 COMMENT '修改人id',
  `gmt_modified` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '申领明细' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_apply_dtl
-- ----------------------------
INSERT INTO `pub_apply_dtl` VALUES (1273142285037084674, 1, 1273142284642820098, 1, 0, 0, '1273142284642820098', '1273142285037084674', 348, 'G202006001', '', '7060     R1 2*80ml   R2 2*20ml', '北京利德曼生化股份有限公司', '盒', 0.0000, 1, 1, 2, 0, 1, '2020-06-17 14:35:49', 1, '2020-06-17 14:35:49');
INSERT INTO `pub_apply_dtl` VALUES (1273142967785889793, 1, 1273142967777501186, 0, 0, 0, '8eeee459-ad9f-47a0-aa2f-3718217444b6', 'f82b55c8-4a2a-43bf-a28f-279b39e76f02', 348, 'G202006001', '肌酸激酶MB同工酶测定试剂盒-CK-MB', '7060     R1 2*80ml   R2 2*20ml', '北京利德曼生化股份有限公司', '盒', 0.0000, 1, 1, 2, 0, 0, '2020-06-17 14:38:32', 0, '2020-06-17 14:38:32');
INSERT INTO `pub_apply_dtl` VALUES (1273144725039882241, 1, 1273144724985356290, 1, 0, 0, '1273144724985356290', '1273144725039882241', 348, 'G202006001', '', '7060     R1 2*80ml   R2 2*20ml', '北京利德曼生化股份有限公司', '盒', 0.0000, 1, 1, 2, 0, 1, '2020-06-17 14:45:31', 1, '2020-06-17 14:45:31');
INSERT INTO `pub_apply_dtl` VALUES (1273145160807096321, 1, 1273145160748376065, 1, 0, 0, '1273145160748376065', '1273145160807096321', 348, 'G202006001', '', '7060     R1 2*80ml   R2 2*20ml', '北京利德曼生化股份有限公司', '盒', 0.0000, 1, 1, 2, 0, 1, '2020-06-17 14:47:15', 1, '2020-06-17 14:47:15');
INSERT INTO `pub_apply_dtl` VALUES (1273158965146550274, 1, 1273158964836171777, 1, 0, 0, '1273158964836171777', '1273158965146550274', 350, 'G202006003', '', 'R1:45ML*2', '重庆中元生物技术有限公司', '盒', 0.0000, 1, 1, 2, 0, 1, '2020-06-17 15:42:06', 1, '2020-06-17 15:42:06');
INSERT INTO `pub_apply_dtl` VALUES (1273159137960263682, 1, 1273159137905737729, 1, 0, 0, '1273159137905737729', '1273159137960263682', 350, 'G202006003', '', 'R1:45ML*2', '重庆中元生物技术有限公司', '盒', 0.0000, 1, 1, 2, 0, 1, '2020-06-17 15:42:47', 1, '2020-06-17 15:42:47');
INSERT INTO `pub_apply_dtl` VALUES (1273195941820235777, 1, 1273195941811847169, 0, 0, 0, '934ce474-55b2-4fbc-b83b-636252c1bd1b', 'dd5eb31a-ebf9-464a-a226-7ed43b68ab50', 348, 'G202006001', '肌酸激酶MB同工酶测定试剂盒-CK-MB', '7060     R1 2*80ml   R2 2*20ml', '北京利德曼生化股份有限公司', '盒', 0.0000, 2, 2, 2, 0, 0, '2020-06-17 18:09:02', 0, '2020-06-17 18:09:02');
INSERT INTO `pub_apply_dtl` VALUES (1273199130674069506, 1, 1273199130623737858, 1, 0, 0, '1273199130623737858', '1273199130674069506', 352, 'G20200615', '', 'R1: 4×40mL+R2: 4×40mL；', '贝克曼库尔特实验系统(苏州)有限公司', '盒', 0.0000, 1, 1, 2, 0, 1, '2020-06-17 18:21:42', 1, '2020-06-17 18:21:42');
INSERT INTO `pub_apply_dtl` VALUES (1278578827641618434, 1, 1278578827633229825, 0, 0, 0, '0673f0e8-e645-4b57-a9c8-833acf63b0fb', '44220a77-4ea5-4003-996c-9088a3e7b539', 353, 'C202006001', '洗脱缓冲液（标准模式', '800ML', '东曹株式会社', '瓶', 0.0000, 2, 0, 1, 0, 0, '2020-07-02 14:38:42', 0, '2020-07-02 14:38:42');

-- ----------------------------
-- Table structure for pub_batch
-- ----------------------------
DROP TABLE IF EXISTS `pub_batch`;
CREATE TABLE `pub_batch`  (
  `id` bigint(20) NOT NULL COMMENT 'id',
  `organ_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '机构ID',
  `goods_id` bigint(20) NOT NULL COMMENT '商品ID',
  `lot_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '批号ID',
  `supply_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '供应商ID',
  `stock_in_date` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '生成时间',
  `price` double(16, 4) NOT NULL DEFAULT 0.0000 COMMENT '单价',
  `source_type` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=送货单',
  `source_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '来源单ID',
  `source_no` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '来源单号',
  `source_dtl_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '来源明细ID',
  `origin_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '原始单ID',
  `origin_dtl_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '原始明细ID',
  `origin_batch_id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '中台批次ID',
  `batch_no` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '批号编码',
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE INDEX `batch_index`(`organ_id`, `goods_id`, `origin_batch_id`) USING BTREE COMMENT '批次组合索引'
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '批次' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_batch
-- ----------------------------
INSERT INTO `pub_batch` VALUES (1273141198578130946, 1, 348, 1273141198414553090, 32, '2020-06-17 14:31:30', 220.0000, 0, 1273141198527799298, 'PO2020061700008', 1273141198565548034, '1273141198527799298', '1273141198565548034', '93014570-039b-43bb-8cb5-cdb4e69de851', '202006170009');
INSERT INTO `pub_batch` VALUES (1273145056515727361, 1, 348, 1273145056473784321, 35, '2020-06-17 14:46:50', 220.0000, 0, 1273145056507338754, 'PO2020061700009', 1273145056511533058, '1273145056507338754', '1273145056511533058', 'b8482b4b-6774-4a49-9ba0-58c9fed6d636', '202006170010');
INSERT INTO `pub_batch` VALUES (1273147782804938754, 1, 350, 1273147782771384322, 36, '2020-06-17 14:57:40', 600.0000, 0, 1273147782796550145, 'PO2020061700010', 1273147782800744449, '1273147782796550145', '1273147782800744449', '538a6494-cd83-40c1-8c10-3133eb639adb', '202006170011');
INSERT INTO `pub_batch` VALUES (1273155039902957570, 1, 348, 1273155039689048065, 36, '2020-06-17 15:26:30', 220.0000, 0, 1273155039835848705, 'PO2020061700011', 1273155039877791746, '1273155039835848705', '1273155039877791746', '6aa8261f-1012-4091-9991-ed76e1ac717c', '202006170012');
INSERT INTO `pub_batch` VALUES (1273155040251084802, 1, 352, 1273155040196558849, 42, '2020-06-17 15:26:30', 1000.0000, 0, 1273155040234307585, 'PO2020061700012', 1273155040242696193, '1273155040234307585', '1273155040242696193', 'f3f054c9-4a25-4ec1-a1dc-8567e238f667', '202006170013');
INSERT INTO `pub_batch` VALUES (1273160869037604866, 1, 348, 1273160868995661825, 36, '2020-06-17 15:49:40', 220.0000, 0, 1273160869029216258, 'PO2020061700013', 1273160869033410561, '1273160869029216258', '1273160869033410561', '3a7f17ee-2bac-4879-93f5-b059cf2d0aca', '202006170014');
INSERT INTO `pub_batch` VALUES (1273184650774839298, 1, 348, 1273184650732896258, 37, '2020-06-17 17:24:10', 220.0000, 0, 1273184650749673474, 'PO2020061700014', 1273184650766450689, '1273184650749673474', '1273184650766450689', '29c300fe-f148-4eb7-9304-7069151373f0', '202006170015');
INSERT INTO `pub_batch` VALUES (1273499013872222210, 1, 350, 1273499013817696258, 35, '2020-06-18 14:13:20', 600.0000, 0, 1273499013859639297, 'PO2020061800001', 1273499013868027906, '1273499013859639297', '1273499013868027906', 'd7d0a4cc-86d8-4fb5-a803-88f89ff572a9', '202006180000');

-- ----------------------------
-- Table structure for pub_delivery_note
-- ----------------------------
DROP TABLE IF EXISTS `pub_delivery_note`;
CREATE TABLE `pub_delivery_note`  (
  `id` bigint(20) NOT NULL COMMENT 'id',
  `organ_id` bigint(20) NOT NULL COMMENT '机构ID',
  `source_type` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=中台',
  `source_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '来源单ID',
  `source_no` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '来源单号',
  `origin_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '原始单ID',
  `supply_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '供应商ID',
  `supply_name` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '供应商名称',
  `supply_time` datetime(0) NULL DEFAULT NULL COMMENT '送货时间',
  `storehouse_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '收货库房',
  `status` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=初始 1=已验收 99=作废',
  `is_delete` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=正常 1=删除',
  `creater_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '创建人id',
  `gmt_create` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `modifier_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '修改人id',
  `gmt_modified` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '送货单' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_delivery_note
-- ----------------------------
INSERT INTO `pub_delivery_note` VALUES (1273141198527799298, 1, 0, 0, 'PO2020061700008', 'PO2020061700008', 32, '广西慧篆商贸有限公司', '2020-06-17 14:31:24', 34, 0, 0, 0, '2020-06-17 14:31:30', 0, '2020-06-17 14:31:30');
INSERT INTO `pub_delivery_note` VALUES (1273145056507338754, 1, 0, 0, 'PO2020061700009', 'PO2020061700009', 35, '广西真世好生物科技', '2020-06-17 14:46:37', 34, 0, 0, 0, '2020-06-17 14:46:50', 0, '2020-06-17 14:46:50');
INSERT INTO `pub_delivery_note` VALUES (1273147782796550145, 1, 0, 0, 'PO2020061700010', 'PO2020061700010', 36, '广西信禾通医疗投资有限公司', '2020-06-17 14:57:23', 34, 0, 0, 0, '2020-06-17 14:57:40', 0, '2020-06-17 14:57:40');
INSERT INTO `pub_delivery_note` VALUES (1273155039835848705, 1, 0, 0, 'PO2020061700011', 'PO2020061700011', 36, '广西信禾通医疗投资有限公司', '2020-06-17 15:08:16', 34, 0, 0, 0, '2020-06-17 15:26:30', 0, '2020-06-17 15:26:30');
INSERT INTO `pub_delivery_note` VALUES (1273155040234307585, 1, 0, 0, 'PO2020061700012', 'PO2020061700012', 42, '广州市伟之晨科仪器械有限公司', '2020-06-17 15:13:21', 34, 0, 0, 0, '2020-06-17 15:26:30', 0, '2020-06-17 15:26:30');
INSERT INTO `pub_delivery_note` VALUES (1273160869029216258, 1, 0, 0, 'PO2020061700013', 'PO2020061700013', 36, '广西信禾通医疗投资有限公司', '2020-06-17 15:49:28', 34, 0, 0, 0, '2020-06-17 15:49:40', 0, '2020-06-17 15:49:40');
INSERT INTO `pub_delivery_note` VALUES (1273184650749673474, 1, 0, 0, 'PO2020061700014', 'PO2020061700014', 37, '南宁乾美商贸有限公司', '2020-06-17 17:24:04', 34, 0, 0, 0, '2020-06-17 17:24:10', 0, '2020-06-17 17:24:10');
INSERT INTO `pub_delivery_note` VALUES (1273499013859639297, 1, 0, 0, 'PO2020061800001', 'PO2020061800001', 35, '广西真世好生物科技', '2020-06-18 14:13:10', 34, 0, 0, 0, '2020-06-18 14:13:20', 0, '2020-06-18 14:13:20');

-- ----------------------------
-- Table structure for pub_delivery_note_dtl
-- ----------------------------
DROP TABLE IF EXISTS `pub_delivery_note_dtl`;
CREATE TABLE `pub_delivery_note_dtl`  (
  `id` bigint(20) NOT NULL COMMENT 'id',
  `delivery_id` bigint(20) NOT NULL COMMENT '送货表ID',
  `organ_id` bigint(20) NOT NULL COMMENT '机构ID',
  `source_type` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=中台',
  `source_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '来源单ID',
  `source_dtl_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '来源明细ID',
  `source_no` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '来源单号',
  `origin_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '原始单ID',
  `origin_dtl_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '原始明细ID',
  `goods_id` bigint(20) NOT NULL COMMENT '商品ID',
  `delivery_qty` int(11) NOT NULL COMMENT '送货数量',
  `lot_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '批号ID',
  `lot_no` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '批号',
  `batch_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '批次',
  `price` double(16, 4) NOT NULL DEFAULT 0.0000 COMMENT '单价',
  `pprod_date` date NULL DEFAULT NULL COMMENT '生产日期',
  `pvalid_date` date NULL DEFAULT NULL COMMENT '有效日期至',
  `status` tinyint(3) NOT NULL DEFAULT 0 COMMENT '0=初始 1=已生成验收单 99=作废',
  `is_delete` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=正常 1=删除',
  `creater_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '创建人id',
  `gmt_create` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `modifier_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '修改人id',
  `gmt_modified` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE INDEX `delivery_note_dtl_index`(`organ_id`, `source_type`, `origin_dtl_id`) USING BTREE COMMENT '收货细单组合索引'
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '送货明细' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_delivery_note_dtl
-- ----------------------------
INSERT INTO `pub_delivery_note_dtl` VALUES (1273141198565548034, 1273141198527799298, 1, 0, 0, 0, 'PO2020061700008', 'PO2020061700008', '82c011e5-ef0a-44f9-ac0e-c1c76e5e6892', 348, 2, 1273141198414553090, '0617-010', 1273141198578130946, 220.0000, '2019-10-14', '2021-01-18', 0, 0, 0, '2020-06-17 14:31:30', 0, '2020-06-17 14:31:30');
INSERT INTO `pub_delivery_note_dtl` VALUES (1273145056511533058, 1273145056507338754, 1, 0, 0, 0, 'PO2020061700009', 'PO2020061700009', '8a8ebb89-c907-4cba-b76f-04866e756a72', 348, 1, 1273145056473784321, '0617-011', 1273145056515727361, 220.0000, '2019-12-24', '2021-03-24', 0, 0, 0, '2020-06-17 14:46:50', 0, '2020-06-17 14:46:50');
INSERT INTO `pub_delivery_note_dtl` VALUES (1273147782800744449, 1273147782796550145, 1, 0, 0, 0, 'PO2020061700010', 'PO2020061700010', '1e73b7de-2b14-406b-9a96-4bf9404cfe42', 350, 3, 1273147782771384322, '0617-12', 1273147782804938754, 600.0000, '2019-08-13', '2021-01-21', 0, 0, 0, '2020-06-17 14:57:40', 0, '2020-06-17 14:57:40');
INSERT INTO `pub_delivery_note_dtl` VALUES (1273155039877791746, 1273155039835848705, 1, 0, 0, 0, 'PO2020061700011', 'PO2020061700011', 'f58ad8dc-8d41-4477-be6b-2c216dcc22dc', 348, 3, 1273155039689048065, '0617-13', 1273155039902957570, 220.0000, '2019-10-21', '2021-04-13', 0, 0, 0, '2020-06-17 15:26:30', 0, '2020-06-17 15:26:30');
INSERT INTO `pub_delivery_note_dtl` VALUES (1273155040242696193, 1273155040234307585, 1, 0, 0, 0, 'PO2020061700012', 'PO2020061700012', '76e2f2f9-f185-4fb9-9fdf-c5ceed9c1b80', 352, 3, 1273155040196558849, '0617-015', 1273155040251084802, 1000.0000, '2019-12-09', '2020-12-16', 0, 0, 0, '2020-06-17 15:26:30', 0, '2020-06-17 15:26:30');
INSERT INTO `pub_delivery_note_dtl` VALUES (1273160869033410561, 1273160869029216258, 1, 0, 0, 0, 'PO2020061700013', 'PO2020061700013', '482f2bc8-4186-428e-9588-163f63ff3899', 348, 1, 1273160868995661825, '0617-016', 1273160869037604866, 220.0000, '2020-01-14', '2021-02-17', 0, 0, 0, '2020-06-17 15:49:40', 0, '2020-06-17 15:49:40');
INSERT INTO `pub_delivery_note_dtl` VALUES (1273184650766450689, 1273184650749673474, 1, 0, 0, 0, 'PO2020061700014', 'PO2020061700014', 'c88f315c-fb64-4380-829f-508f372da86f', 348, 2, 1273184650732896258, '0617-020', 1273184650774839298, 220.0000, '2019-12-16', '2020-12-17', 0, 0, 0, '2020-06-17 17:24:10', 0, '2020-06-17 17:24:10');
INSERT INTO `pub_delivery_note_dtl` VALUES (1273499013868027906, 1273499013859639297, 1, 0, 0, 0, 'PO2020061800001', 'PO2020061800001', 'a31f963d-f441-4431-82b7-13660d14794d', 350, 3, 1273499013817696258, '0618-001', 1273499013872222210, 600.0000, '2019-11-06', '2021-06-23', 0, 0, 0, '2020-06-18 14:13:20', 0, '2020-06-18 14:13:20');

-- ----------------------------
-- Table structure for pub_goods
-- ----------------------------
DROP TABLE IF EXISTS `pub_goods`;
CREATE TABLE `pub_goods`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'id',
  `organ_id` bigint(20) NULL DEFAULT 0 COMMENT '机构id',
  `origin_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '原DSC商品id',
  `goods_code` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '商品编码',
  `goods_name` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '商品名称',
  `goods_common_name` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '商品通用名称',
  `goods_bar_code` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '商品条形码(DI码)',
  `goods_spec` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '商品规格',
  `goods_unit` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '商品单位',
  `goods_factory_id` bigint(20) NULL DEFAULT NULL COMMENT '商品厂家Id',
  `goods_factory_name` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '商品厂家名称',
  `goods_prodarea` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '商品产地',
  `goods_pack_size` decimal(10, 4) NULL DEFAULT 1.0000 COMMENT '商品包装大小',
  `goods_forecast_days` decimal(10, 0) NULL DEFAULT NULL COMMENT '商品效期预警天数',
  `goods_type` tinyint(4) NULL DEFAULT 0 COMMENT '商品类型(0:耗材,1:试剂,2:药品)',
  `goods_status` tinyint(1) NULL DEFAULT 0 COMMENT '商品状态(0:禁用,1:启用)',
  `goods_memo` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '商品备注',
  `is_delete` tinyint(1) NULL DEFAULT 0 COMMENT '是否删除(0:否,1:是)',
  `creater_id` bigint(20) NULL DEFAULT 0 COMMENT '创建人id',
  `gmt_create` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `modifier_id` bigint(20) NULL DEFAULT 0 COMMENT '修改人id',
  `gmt_modified` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  `goods_source_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '原DSC商品id',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_goods_code`(`goods_bar_code`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 354 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '商品表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_goods
-- ----------------------------
INSERT INTO `pub_goods` VALUES (344, 1, '036f7962-6582-4a5f-bc89-c38d7570fc7c', 'NNSE8001339', '(CKMB)肌酸激酶同工酶MB检测试剂盒[比色法]', '肌酸激酶同工酶MB检测试剂盒[比色法]', 'NNSE8001339', '60test', '盒', NULL, '德国罗氏诊断有限公司', '', 1.0000, 15, 0, 1, '', 0, 0, '2020-06-10 10:21:16', 0, '2020-06-10 10:21:24', '036f7962-6582-4a5f-bc89-c38d7570fc7c');
INSERT INTO `pub_goods` VALUES (345, 1, 'fb8b819a-05a2-4641-b1e8-6ca83ff9f5d1', 'NNSE8010234', '(Mg)镁液体试剂盒', '镁液体试剂盒(MG)', 'NNSE8010234', 'R1:8*70ml', '盒', NULL, '德赛诊断系统(上海)有限公司', '', 1.0000, 15, 0, 1, '', 0, 0, '2020-06-10 10:29:35', 0, '2020-06-10 10:29:44', 'fb8b819a-05a2-4641-b1e8-6ca83ff9f5d1');
INSERT INTO `pub_goods` VALUES (346, 1, '3edb6e71-0866-486c-b930-fd0d248dab85', 'NNSE8000871', '(APTT)活化部分凝血活酶时间试剂', '活化部分凝血活酶时间(APTT)试剂', 'NNSE8000871', '12*5ML', '盒', NULL, '法国思达高', '', 1.0000, 30, 0, 1, '', 0, 0, '2020-06-10 10:34:10', 0, '2020-06-10 10:34:14', '3edb6e71-0866-486c-b930-fd0d248dab85');
INSERT INTO `pub_goods` VALUES (347, 1, '004a0dc8-3969-4c13-84c0-ec8ae67dcfd7', 'NNSE0017723', 'AVE-76系列尿液有形成份分析仪试剂包12', 'AVE-76系列尿液有形成份分析仪试剂包', 'NNSE0017723', '10L', '桶', NULL, '爱威科技股份有限公司', '', 1.0000, 60, 0, 1, '', 0, 0, '2020-06-10 10:46:34', 0, '2020-06-10 10:46:34', '004a0dc8-3969-4c13-84c0-ec8ae67dcfd7');
INSERT INTO `pub_goods` VALUES (348, 1, '955f6724-7dab-4009-9c62-a2d63ef30a20', 'G202006001', '肌酸激酶MB同工酶测定试剂盒-CK-MB', '', 'G202006001', '7060     R1 2*80ml   R2 2*20ml', '盒', NULL, '北京利德曼生化股份有限公司', '', 1.0000, 100, 0, 1, '', 0, 0, '2020-06-10 14:43:30', 0, '2020-06-10 14:43:34', '955f6724-7dab-4009-9c62-a2d63ef30a20');
INSERT INTO `pub_goods` VALUES (349, 1, '22412ece-ebd5-4525-9678-584d1165ba92', 'G202006002', '中性粒细胞明胶酶相关脂质运载蛋白测定试剂盒', '', 'G202006002', '试剂1(R1）：20ml*2 试剂2(R2）：10ml*1', '盒', NULL, '中生北控生物科技股份有限公司', '', 1.0000, 50, 0, 1, '', 0, 0, '2020-06-10 14:43:44', 0, '2020-06-10 14:43:44', '22412ece-ebd5-4525-9678-584d1165ba92');
INSERT INTO `pub_goods` VALUES (350, 1, '334008f8-38b8-427b-86b1-6b2025b7f0b3', 'G202006003', '纤维连接蛋白检测试剂盒', '', 'G202006003', 'R1:45ML*2', '盒', NULL, '重庆中元生物技术有限公司', '', 1.0000, 0, 0, 1, '', 0, 0, '2020-06-12 11:20:27', 0, '2020-06-12 11:20:34', '334008f8-38b8-427b-86b1-6b2025b7f0b3');
INSERT INTO `pub_goods` VALUES (351, 1, 'e24c285b-8b6c-467d-bf61-12761844bd3d', 'G202006004', '总β亚单位人绒毛膜促性腺激素测定试剂盒', '', 'G202006004', '2×50个测试/盒', '盒', NULL, '贝克曼库尔特商贸(中国)有限公司', '', 1.0000, 0, 0, 1, '', 0, 0, '2020-06-12 14:59:23', 0, '2020-06-12 14:59:24', 'e24c285b-8b6c-467d-bf61-12761844bd3d');
INSERT INTO `pub_goods` VALUES (352, 1, '90ea26ba-248e-475a-806f-12a23bc96995', 'G20200615', 'γ-谷氨酰氨基转移酶测定试剂盒', '', 'G20200615', 'R1: 4×40mL+R2: 4×40mL；', '盒', NULL, '贝克曼库尔特实验系统(苏州)有限公司', '', 1.0000, 120, 0, 1, '', 0, 0, '2020-06-15 16:07:49', 0, '2020-06-15 16:07:54', '90ea26ba-248e-475a-806f-12a23bc96995');
INSERT INTO `pub_goods` VALUES (353, 1, 'cc6c1830-6afb-43bc-8eb5-827ec98aa89d', 'C202006001', '洗脱缓冲液（标准模式', '', 'C202006001', '800ML', '瓶', NULL, '东曹株式会社', '', 1.0000, 0, 0, 1, '', 0, 0, '2020-06-15 16:48:53', 0, '2020-06-15 16:48:54', 'cc6c1830-6afb-43bc-8eb5-827ec98aa89d');

-- ----------------------------
-- Table structure for pub_loc
-- ----------------------------
DROP TABLE IF EXISTS `pub_loc`;
CREATE TABLE `pub_loc`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'id',
  `organ_id` bigint(20) NULL DEFAULT 0 COMMENT '机构id',
  `storehouse_id` bigint(20) NULL DEFAULT NULL COMMENT '所属库房id',
  `zone_id` bigint(20) NULL DEFAULT NULL COMMENT '所属区域id',
  `loc_code` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '0' COMMENT '货位编码 ',
  `loc_addr` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '货位位置',
  `loc_status` tinyint(4) NULL DEFAULT 1 COMMENT '货位状态(0:禁用,1:启用)',
  `loc_memo` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '货位备注',
  `is_delete` tinyint(1) NULL DEFAULT 0 COMMENT '是否删除(0:否,1:是)',
  `creater_id` bigint(20) NULL DEFAULT 0 COMMENT '创建人id',
  `gmt_create` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `modifier_id` bigint(20) NULL DEFAULT 0 COMMENT '修改人id',
  `gmt_modified` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_storehouse_id`(`storehouse_id`) USING BTREE,
  INDEX `idx_zone_id`(`zone_id`) USING BTREE,
  INDEX `idx_loc_code`(`loc_code`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 277 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '货位表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_loc
-- ----------------------------
INSERT INTO `pub_loc` VALUES (262, 1, 25, 350, 'LOC_ZoneC001', '', 1, '', 1, 0, '2020-06-10 14:13:26', 0, '2020-06-10 14:48:36');
INSERT INTO `pub_loc` VALUES (263, 1, 26, 351, 'LOC_ZoneL001', '', 1, '', 1, 0, '2020-06-10 14:15:56', 0, '2020-06-10 14:48:56');
INSERT INTO `pub_loc` VALUES (264, 1, 27, 352, 'LOC_ZoneL002', '', 1, '', 1, 0, '2020-06-10 14:16:56', 0, '2020-06-10 14:48:46');
INSERT INTO `pub_loc` VALUES (265, 1, 28, 353, 'LOC_ZoneC004', '', 1, '', 1, 0, '2020-06-10 14:18:46', 0, '2020-06-10 14:48:46');
INSERT INTO `pub_loc` VALUES (266, 1, 29, 354, 'LOC_ZoneC0000', '', 1, '', 1, 0, '2020-06-10 14:28:36', 0, '2020-06-10 14:48:36');
INSERT INTO `pub_loc` VALUES (267, 1, 30, 355, 'LOC_Zone201', '', 1, '', 0, 0, '2020-06-10 14:32:46', 0, '2020-06-10 14:32:46');
INSERT INTO `pub_loc` VALUES (268, 1, 31, 356, 'LOC_Zone301', '', 1, '', 0, 0, '2020-06-10 14:34:46', 0, '2020-06-10 14:34:46');
INSERT INTO `pub_loc` VALUES (269, 1, 32, 357, 'LOC_Zone401', '', 1, '', 0, 0, '2020-06-10 14:35:26', 0, '2020-06-10 14:35:26');
INSERT INTO `pub_loc` VALUES (270, 1, 33, 358, 'LOC_Zone402', '', 1, '', 0, 0, '2020-06-10 14:36:36', 0, '2020-06-10 14:36:36');
INSERT INTO `pub_loc` VALUES (271, 1, 34, 367, 'LOC_Zone201', '', 1, '', 0, 0, '2020-06-12 09:52:56', 1, '2020-06-15 10:47:39');
INSERT INTO `pub_loc` VALUES (272, 1, 35, 368, 'LOC_Zone301', '', 1, '', 0, 0, '2020-06-12 09:53:06', 0, '2020-06-12 09:53:06');
INSERT INTO `pub_loc` VALUES (273, 1, 36, 369, 'LOC_Zone402', '', 1, '', 0, 0, '2020-06-12 09:53:16', 1, '2020-06-15 10:47:20');
INSERT INTO `pub_loc` VALUES (274, 1, 37, 370, 'LOC_Zone401', '', 1, '', 0, 0, '2020-06-12 09:53:26', 1, '2020-06-15 10:47:05');
INSERT INTO `pub_loc` VALUES (275, 1, 34, 371, '5465', '', 0, '范德萨', 0, 1, '2020-06-15 10:41:28', 1, '2020-06-15 10:41:28');
INSERT INTO `pub_loc` VALUES (276, 1, 34, 371, '范德萨', '', 1, '', 0, 1, '2020-06-15 10:41:37', 1, '2020-06-15 10:41:37');

-- ----------------------------
-- Table structure for pub_lot
-- ----------------------------
DROP TABLE IF EXISTS `pub_lot`;
CREATE TABLE `pub_lot`  (
  `id` bigint(20) NOT NULL COMMENT 'id',
  `lot_no` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '批号',
  `organ_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '机构ID',
  `goods_id` bigint(20) NOT NULL COMMENT '商品ID',
  `pprod_date` date NULL DEFAULT NULL COMMENT '生产日期',
  `pvalid_date` date NULL DEFAULT NULL COMMENT '有效日期至',
  `invalid_date` date NULL DEFAULT NULL COMMENT '失效期',
  `is_delete` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=正常 1=删除',
  `creater_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '创建人id',
  `gmt_create` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `modifier_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '修改人id',
  `gmt_modified` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE INDEX `lot_index`(`lot_no`, `organ_id`, `goods_id`) USING BTREE COMMENT '批号组合索引'
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '批号' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_lot
-- ----------------------------
INSERT INTO `pub_lot` VALUES (1273141198414553090, '0617-010', 1, 348, '2019-10-14', '2021-01-18', '2021-01-17', 3, 0, '2020-06-17 14:31:30', 0, '2020-06-17 14:31:30');
INSERT INTO `pub_lot` VALUES (1273145056473784321, '0617-011', 1, 348, '2019-12-24', '2021-03-24', '2021-03-23', 3, 0, '2020-06-17 14:46:50', 0, '2020-06-17 14:46:50');
INSERT INTO `pub_lot` VALUES (1273147782771384322, '0617-12', 1, 350, '2019-08-13', '2021-01-21', '2021-01-20', 3, 0, '2020-06-17 14:57:40', 0, '2020-06-17 14:57:40');
INSERT INTO `pub_lot` VALUES (1273155039689048065, '0617-13', 1, 348, '2019-10-21', '2021-04-13', '2021-04-12', 3, 0, '2020-06-17 15:26:30', 0, '2020-06-17 15:26:30');
INSERT INTO `pub_lot` VALUES (1273155040196558849, '0617-015', 1, 352, '2019-12-09', '2020-12-16', '2020-12-15', 3, 0, '2020-06-17 15:26:30', 0, '2020-06-17 15:26:30');
INSERT INTO `pub_lot` VALUES (1273160868995661825, '0617-016', 1, 348, '2020-01-14', '2021-02-17', '2021-02-16', 3, 0, '2020-06-17 15:49:40', 0, '2020-06-17 15:49:40');
INSERT INTO `pub_lot` VALUES (1273184650732896258, '0617-020', 1, 348, '2019-12-16', '2020-12-17', '2020-12-16', 3, 0, '2020-06-17 17:24:10', 0, '2020-06-17 17:24:10');
INSERT INTO `pub_lot` VALUES (1273499013817696258, '0618-001', 1, 350, '2019-11-06', '2021-06-23', '2021-06-22', 3, 0, '2020-06-18 14:13:20', 0, '2020-06-18 14:13:20');

-- ----------------------------
-- Table structure for pub_office
-- ----------------------------
DROP TABLE IF EXISTS `pub_office`;
CREATE TABLE `pub_office`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'id',
  `pid` bigint(20) NULL DEFAULT NULL COMMENT '父科室Id',
  `organ_id` bigint(20) NULL DEFAULT 0 COMMENT '机构id',
  `office_source_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '原科室id',
  `office_code` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '科室编码',
  `office_name` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '科室名称',
  `office_addr` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '科室地址',
  `office_status` tinyint(1) NULL DEFAULT 0 COMMENT '科室状态(0:禁用,1:启用)',
  `office_memo` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '科室备注',
  `head` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '科室负责人',
  `contact_number` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '科室联系电话',
  `is_delete` tinyint(1) NULL DEFAULT 0 COMMENT '是否删除(0:否,1:是)',
  `creater_id` bigint(20) NULL DEFAULT 0 COMMENT '创建人id',
  `gmt_create` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `modifier_id` bigint(20) NULL DEFAULT 0 COMMENT '修改人id',
  `gmt_modified` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  `origin_id` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '源数据id',
  `office_type` tinyint(4) NULL DEFAULT 0 COMMENT '科室类型',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_office_no`(`office_code`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1271260371124707330 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '科室表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_office
-- ----------------------------
INSERT INTO `pub_office` VALUES (1271259825869381633, NULL, 1, '0000000000', '0000000000', '试剂中心部门', '', 1, '', NULL, NULL, 0, 0, '1970-01-01 08:00:00', 0, '2020-06-12 10:18:16', '260c7f06-092d-4b1e-9e10-027c7c59dfdf', 1);
INSERT INTO `pub_office` VALUES (1271259909738684418, NULL, 1, '0000000001', '0000000001', '医学检验科', '', 1, '', NULL, NULL, 0, 0, '1970-01-01 08:00:00', 0, '2020-06-12 09:55:56', '8d7a24e8-1e54-4178-9c55-8324e9ee6e48', 1);
INSERT INTO `pub_office` VALUES (1271260245287198721, 1271259909738684418, 1, '0000000002', '0000000002', '检验组', '', 1, '', NULL, NULL, 0, 0, '1970-01-01 08:00:00', 0, '2020-06-12 09:57:16', 'ab4995e7-4cc4-4872-b8ed-f4b519ed82a3', 2);
INSERT INTO `pub_office` VALUES (1271260329185861633, 1271259825869381633, 1, '0000000003', '0000000003', '冷藏库一', '', 1, '', NULL, NULL, 0, 0, '1970-01-01 08:00:00', 0, '2020-06-12 09:57:36', 'dab3a9e3-f9b1-4c30-8067-ff1e97fbf03d', 2);
INSERT INTO `pub_office` VALUES (1271260371124707329, 1271259825869381633, 1, '0000000004', '0000000004', '冷藏库二', '', 1, '', NULL, NULL, 0, 0, '1970-01-01 08:00:00', 0, '2020-06-12 09:57:46', '8fbe7280-5a3b-4e4d-9652-e8afadd2f975', 2);

-- ----------------------------
-- Table structure for pub_organ
-- ----------------------------
DROP TABLE IF EXISTS `pub_organ`;
CREATE TABLE `pub_organ`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'id',
  `organ_source_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '原机构id',
  `organ_code` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '机构编码',
  `organ_name` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '机构名称',
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE INDEX `uk_usercode`(`organ_code`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '医院机构表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_organ
-- ----------------------------
INSERT INTO `pub_organ` VALUES (1, 'a09edb3b-5796-11ea-9cee-0894ef5a480a', '00001', 'XXXX医院');

-- ----------------------------
-- Table structure for pub_pick
-- ----------------------------
DROP TABLE IF EXISTS `pub_pick`;
CREATE TABLE `pub_pick`  (
  `id` bigint(20) NOT NULL COMMENT 'id',
  `organ_id` bigint(20) NOT NULL COMMENT '机构ID',
  `source_type` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=申领 1=退货',
  `source_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '来源单ID',
  `source_no` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '来源单号',
  `origin_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '原始单ID',
  `storehouse_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '库房',
  `pick_person_id` bigint(20) NULL DEFAULT NULL COMMENT '拣选人ID',
  `pick_person_name` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '拣选人',
  `status` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=初始 1=已拣选 99=作废',
  `is_delete` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=正常 1=删除',
  `creater_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '创建人id',
  `gmt_create` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `modifier_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '修改人id',
  `gmt_modified` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '拣选单' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_pick
-- ----------------------------
INSERT INTO `pub_pick` VALUES (1273142967957856258, 1, 0, 1273142967777501186, 'WA2020061700007', '8eeee459-ad9f-47a0-aa2f-3718217444b6', 37, 1, 'admin', 1, 0, 0, '2020-06-17 14:38:32', 0, '2020-06-17 14:38:32');
INSERT INTO `pub_pick` VALUES (1273195941992202241, 1, 0, 1273195941811847169, 'WA2020061700011', '934ce474-55b2-4fbc-b83b-636252c1bd1b', 37, 1, 'admin', 1, 0, 0, '2020-06-17 18:09:02', 0, '2020-06-17 18:09:02');

-- ----------------------------
-- Table structure for pub_pick_dtl
-- ----------------------------
DROP TABLE IF EXISTS `pub_pick_dtl`;
CREATE TABLE `pub_pick_dtl`  (
  `id` bigint(20) NOT NULL COMMENT 'id',
  `pick_id` bigint(20) NOT NULL COMMENT '拣选单ID',
  `organ_id` bigint(20) NOT NULL COMMENT '机构ID',
  `source_type` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=申领 1=退货',
  `source_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '来源单ID',
  `source_no` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '来源单号',
  `source_dtl_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '来源明细ID',
  `origin_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '原始单ID',
  `origin_dtl_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '原始明细ID',
  `storehouse_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '库房',
  `loc_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '货位ID',
  `zone_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '区域ID',
  `loc_no` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '货位编码',
  `goods_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '商品ID',
  `goods_name` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '商品名称',
  `goods_spec` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '商品规格',
  `pick_qty` int(11) NOT NULL COMMENT '拣选数量',
  `check_qty` int(11) NOT NULL DEFAULT 0 COMMENT '复核数量',
  `status` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=初始 1=已拣选 99=作废',
  `is_delete` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=正常 1=删除',
  `creater_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '创建人id',
  `gmt_create` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `modifier_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '修改人id',
  `gmt_modified` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '拣选明细' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_pick_dtl
-- ----------------------------
INSERT INTO `pub_pick_dtl` VALUES (1273142967987216386, 1273142967957856258, 1, 0, 1273142967777501186, 'WA2020061700007', 1273142967785889793, 'f82b55c8-4a2a-43bf-a28f-279b39e76f02', 'f82b55c8-4a2a-43bf-a28f-279b39e76f02', 34, 271, 367, 'LOC_Zone201', 348, '肌酸激酶MB同工酶测定试剂盒-CK-MB', '7060     R1 2*80ml   R2 2*20ml', 1, 0, 1, 0, 0, '2020-06-17 14:38:32', 0, '2020-06-17 14:38:32');
INSERT INTO `pub_pick_dtl` VALUES (1273195942029950977, 1273195941992202241, 1, 0, 1273195941811847169, 'WA2020061700011', 1273195941820235777, 'dd5eb31a-ebf9-464a-a226-7ed43b68ab50', 'dd5eb31a-ebf9-464a-a226-7ed43b68ab50', 34, 271, 367, 'LOC_Zone201', 348, '肌酸激酶MB同工酶测定试剂盒-CK-MB', '7060     R1 2*80ml   R2 2*20ml', 2, 0, 1, 0, 0, '2020-06-17 18:09:02', 0, '2020-06-17 18:09:02');
INSERT INTO `pub_pick_dtl` VALUES (1273195942038339585, 1273195941992202241, 1, 0, 1273195941811847169, 'WA2020061700011', 1273195941820235777, 'dd5eb31a-ebf9-464a-a226-7ed43b68ab50', 'dd5eb31a-ebf9-464a-a226-7ed43b68ab50', 34, 271, 367, 'LOC_Zone201', 348, '肌酸激酶MB同工酶测定试剂盒-CK-MB', '7060     R1 2*80ml   R2 2*20ml', 1, 0, 1, 0, 0, '2020-06-17 18:09:02', 0, '2020-06-17 18:09:02');

-- ----------------------------
-- Table structure for pub_rack
-- ----------------------------
DROP TABLE IF EXISTS `pub_rack`;
CREATE TABLE `pub_rack`  (
  `id` bigint(20) NOT NULL COMMENT 'id',
  `organ_id` bigint(20) NOT NULL COMMENT '机构ID',
  `source_type` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=验收单 1=退库单',
  `source_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '来源单ID',
  `source_no` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '来源单号',
  `origin_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '原始单ID',
  `storehouse_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '库房',
  `rack_person_id` bigint(20) NULL DEFAULT NULL COMMENT '上架人ID',
  `rack_person_name` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '上架人',
  `status` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=初始 1=上架中 2=上架完成 99=作废',
  `is_delete` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=正常 1=删除',
  `creater_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '创建人id',
  `gmt_create` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `modifier_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '修改人id',
  `gmt_modified` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '上架单' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_rack
-- ----------------------------
INSERT INTO `pub_rack` VALUES (1273141431496220674, 1, 0, 1273141198527799298, 'PO2020061700008', 'PO2020061700008', 34, 1, 'admin', 0, 0, 1, '2020-06-17 14:32:25', 0, '2020-06-17 14:32:25');
INSERT INTO `pub_rack` VALUES (1273144517438611457, 1, 0, 1273144517371502594, 'AB00000000057', '', 37, 1, 'admin', 0, 0, 1, '2020-06-17 14:44:41', 0, '2020-06-17 14:44:41');
INSERT INTO `pub_rack` VALUES (1273145108214718466, 1, 0, 1273145056507338754, 'PO2020061700009', 'PO2020061700009', 34, 1, 'admin', 0, 0, 1, '2020-06-17 14:47:02', 0, '2020-06-17 14:47:02');
INSERT INTO `pub_rack` VALUES (1273156188039151618, 1, 0, 1273147782796550145, 'PO2020061700010', 'PO2020061700010', 34, 1, 'admin', 0, 0, 1, '2020-06-17 15:31:03', 0, '2020-06-17 15:31:03');
INSERT INTO `pub_rack` VALUES (1273159335302266882, 1, 0, 1273159335239352321, 'AB00000000059', '', 36, 1, 'admin', 0, 0, 1, '2020-06-17 15:43:34', 0, '2020-06-17 15:43:34');
INSERT INTO `pub_rack` VALUES (1273170861455179777, 1, 0, 1273160869029216258, 'PO2020061700013', 'PO2020061700013', 34, 1, 'admin', 0, 0, 1, '2020-06-17 16:29:22', 0, '2020-06-17 16:29:22');
INSERT INTO `pub_rack` VALUES (1273170914383101954, 1, 0, 1273155040234307585, 'PO2020061700012', 'PO2020061700012', 34, 1, 'admin', 0, 0, 1, '2020-06-17 16:29:35', 0, '2020-06-17 16:29:35');
INSERT INTO `pub_rack` VALUES (1273184795813871617, 1, 0, 1273184650749673474, 'PO2020061700014', 'PO2020061700014', 34, 1, 'admin', 0, 0, 1, '2020-06-17 17:24:44', 0, '2020-06-17 17:24:44');

-- ----------------------------
-- Table structure for pub_rack_dtl
-- ----------------------------
DROP TABLE IF EXISTS `pub_rack_dtl`;
CREATE TABLE `pub_rack_dtl`  (
  `id` bigint(20) NOT NULL COMMENT 'id',
  `rack_id` bigint(20) NOT NULL COMMENT '上架单ID',
  `organ_id` bigint(20) NOT NULL COMMENT '机构ID',
  `source_type` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=上架单 1=退库单',
  `source_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '来源单ID',
  `source_no` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '来源单号',
  `source_dtl_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '来源明细ID',
  `origin_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '原始单ID',
  `origin_dtl_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '原始明细ID',
  `storehouse_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '库房',
  `loc_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '货位ID',
  `zone_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '区域ID',
  `loc_no` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '货位编码',
  `goods_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '商品ID',
  `goods_name` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '商品名称',
  `goods_spec` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '商品规格',
  `rack_qty` int(11) NOT NULL COMMENT '上架数量',
  `status` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=初始 1=已上架收 99=作废',
  `is_delete` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=正常 1=删除',
  `creater_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '创建人id',
  `gmt_create` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `modifier_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '修改人id',
  `gmt_modified` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '上架明细' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_rack_dtl
-- ----------------------------
INSERT INTO `pub_rack_dtl` VALUES (1273141431735296001, 1273141431496220674, 1, 0, 1273141198527799298, 'PO2020061700008', 1273141198565548034, 'PO2020061700008', '82c011e5-ef0a-44f9-ac0e-c1c76e5e6892', 34, 271, 367, 'LOC_Zone201', 348, '肌酸激酶MB同工酶测定试剂盒-CK-MB', '7060     R1 2*80ml   R2 2*20ml', 2, 0, 0, 1, '2020-06-17 14:32:25', 1, '2020-06-17 14:32:25');
INSERT INTO `pub_rack_dtl` VALUES (1273144517480554498, 1273144517438611457, 1, 0, 1273144517371502594, 'AB00000000057', 1273144517430222849, '', '', 37, 271, 367, 'LOC_Zone201', 348, '肌酸激酶MB同工酶测定试剂盒-CK-MB', '7060     R1 2*80ml   R2 2*20ml', 1, 0, 0, 1, '2020-06-17 14:44:41', 1, '2020-06-17 14:44:41');
INSERT INTO `pub_rack_dtl` VALUES (1273145108244078594, 1273145108214718466, 1, 0, 1273145056507338754, 'PO2020061700009', 1273145056511533058, 'PO2020061700009', '8a8ebb89-c907-4cba-b76f-04866e756a72', 34, 271, 367, 'LOC_Zone201', 348, '肌酸激酶MB同工酶测定试剂盒-CK-MB', '7060     R1 2*80ml   R2 2*20ml', 1, 0, 0, 1, '2020-06-17 14:47:02', 1, '2020-06-17 14:47:02');
INSERT INTO `pub_rack_dtl` VALUES (1273156188219506689, 1273156188039151618, 1, 0, 1273147782796550145, 'PO2020061700010', 1273147782800744449, 'PO2020061700010', '1e73b7de-2b14-406b-9a96-4bf9404cfe42', 34, 271, 367, 'LOC_Zone201', 350, '纤维连接蛋白检测试剂盒', 'R1:45ML*2', 3, 0, 0, 1, '2020-06-17 15:31:04', 1, '2020-06-17 15:31:04');
INSERT INTO `pub_rack_dtl` VALUES (1273159335344209922, 1273159335302266882, 1, 0, 1273159335239352321, 'AB00000000059', 1273159335298072578, '', '', 36, 271, 367, 'LOC_Zone201', 350, '纤维连接蛋白检测试剂盒', 'R1:45ML*2', 1, 0, 0, 1, '2020-06-17 15:43:34', 1, '2020-06-17 15:43:34');
INSERT INTO `pub_rack_dtl` VALUES (1273170861501317121, 1273170861455179777, 1, 0, 1273160869029216258, 'PO2020061700013', 1273160869033410561, 'PO2020061700013', '482f2bc8-4186-428e-9588-163f63ff3899', 34, 271, 367, 'LOC_Zone201', 348, '肌酸激酶MB同工酶测定试剂盒-CK-MB', '7060     R1 2*80ml   R2 2*20ml', 1, 0, 0, 1, '2020-06-17 16:29:22', 1, '2020-06-17 16:29:22');
INSERT INTO `pub_rack_dtl` VALUES (1273170914404073474, 1273170914383101954, 1, 0, 1273155040234307585, 'PO2020061700012', 1273155040242696193, 'PO2020061700012', '76e2f2f9-f185-4fb9-9fdf-c5ceed9c1b80', 34, 0, 0, '', 352, 'γ-谷氨酰氨基转移酶测定试剂盒', 'R1: 4×40mL+R2: 4×40mL；', 3, 0, 0, 1, '2020-06-17 16:29:35', 1, '2020-06-17 16:29:35');
INSERT INTO `pub_rack_dtl` VALUES (1273184795876786177, 1273184795813871617, 1, 0, 1273184650749673474, 'PO2020061700014', 1273184650766450689, 'PO2020061700014', 'c88f315c-fb64-4380-829f-508f372da86f', 34, 271, 367, 'LOC_Zone201', 348, '肌酸激酶MB同工酶测定试剂盒-CK-MB', '7060     R1 2*80ml   R2 2*20ml', 2, 0, 0, 1, '2020-06-17 17:24:44', 1, '2020-06-17 17:24:44');

-- ----------------------------
-- Table structure for pub_refund
-- ----------------------------
DROP TABLE IF EXISTS `pub_refund`;
CREATE TABLE `pub_refund`  (
  `id` bigint(20) NOT NULL COMMENT 'id',
  `organ_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '机构ID',
  `source_type` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=中台 1=手工',
  `source_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '来源单ID',
  `source_no` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '来源单号',
  `origin_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '原始单ID',
  `supply_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '供应商ID',
  `supply_name` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '供应商',
  `storehouse_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '退货库房ID',
  `storehouse_name` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '退货库房',
  `refund_person_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '退货人ID',
  `refund_person_name` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '退货人',
  `refund_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '退货时间',
  `check_person_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '复核人ID',
  `check_person_name` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '复核人姓名',
  `check_time` datetime(0) NULL DEFAULT NULL COMMENT '复核时间',
  `status` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=初始 1=拣选 2=复核完成 99=作废',
  `is_delete` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=正常 1=删除',
  `creater_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '创建人id',
  `gmt_create` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `modifier_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '修改人id',
  `gmt_modified` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '退货单' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_refund
-- ----------------------------
INSERT INTO `pub_refund` VALUES (1298079682680426497, 1, 0, 0, 'PB2020082500002', '1298079682680426497', 49, '广西康致医疗器械有限责任公司', 12, '试剂中心库', 0, 'admin', '2020-08-25 09:56:22', 0, '', NULL, 1, 0, 0, '2020-08-25 10:08:08', 0, '2020-08-25 10:08:08');
INSERT INTO `pub_refund` VALUES (1298079687264800769, 1, 0, 0, 'PB2020082500003', '1298079687264800769', 49, '广西康致医疗器械有限责任公司', 12, '试剂中心库', 0, 'admin', '2020-08-25 10:02:07', 0, '', NULL, 1, 0, 0, '2020-08-25 10:08:09', 0, '2020-08-25 10:08:09');
INSERT INTO `pub_refund` VALUES (1298084946515169281, 1, 0, 0, 'PB2020082500004', '1298084946515169281', 49, '广西康致医疗器械有限责任公司', 12, '试剂中心库', 0, 'admin', '2020-08-25 10:28:55', 0, '', NULL, 1, 0, 0, '2020-08-25 10:29:03', 0, '2020-08-25 10:29:03');
INSERT INTO `pub_refund` VALUES (1298153523066789890, 1, 0, 0, 'PB2020082500005', '1298153523066789890', 50, '江苏硕世生物科技有限公司', 12, '试剂中心库', 0, 'admin', '2020-08-25 15:01:23', 0, '', NULL, 1, 0, 0, '2020-08-25 15:01:33', 0, '2020-08-25 15:01:33');
INSERT INTO `pub_refund` VALUES (1298160067363823618, 1, 0, 0, 'PB2020082500006', '1298160067363823618', 50, '江苏硕世生物科技有限公司', 12, '试剂中心库', 0, 'admin', '2020-08-25 15:27:25', 0, '', NULL, 1, 0, 0, '2020-08-25 15:27:33', 0, '2020-08-25 15:27:33');

-- ----------------------------
-- Table structure for pub_refund_dtl
-- ----------------------------
DROP TABLE IF EXISTS `pub_refund_dtl`;
CREATE TABLE `pub_refund_dtl`  (
  `id` bigint(20) NOT NULL COMMENT 'id',
  `organ_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '机构ID',
  `refund_id` bigint(20) NOT NULL COMMENT '申领表ID',
  `source_type` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=中台 1=手工',
  `source_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '来源单ID',
  `source_dtl_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '来源明细ID',
  `origin_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '原始单ID',
  `origin_dtl_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '原始明细单ID',
  `goods_id` bigint(20) NOT NULL COMMENT '商品ID',
  `goods_no` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '商品编码',
  `goods_name` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '商品名称',
  `goods_spec` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '商品规格',
  `goods_factory_name` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '生产厂家',
  `goods_unit` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '单位',
  `price` double(16, 4) NOT NULL DEFAULT 0.0000 COMMENT '单价',
  `refund_qty` int(11) NOT NULL DEFAULT 0 COMMENT '退货数量',
  `check_qty` int(11) NOT NULL DEFAULT 0 COMMENT '复核数量',
  `lot_no` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '批号',
  `status` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=初始 1=拣选 2=复核完成 99=作废',
  `is_delete` tinyint(4) NOT NULL DEFAULT 0 COMMENT '0=正常 1=删除',
  `creater_id` bigint(20) NULL DEFAULT 0 COMMENT '创建人id',
  `gmt_create` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `modifier_id` bigint(20) NULL DEFAULT 0 COMMENT '修改人id',
  `gmt_modified` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  `back_reason` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '原因',
  `singlecode` varchar(1024) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '单品码',
  `pprod_date` date NULL DEFAULT NULL COMMENT '生产日期',
  `pvalid_date` date NULL DEFAULT NULL COMMENT '有效日期至',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '退货明细' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_refund_dtl
-- ----------------------------
INSERT INTO `pub_refund_dtl` VALUES (1298079684475588609, 1, 1298079682680426497, 0, 0, 0, '', '269ee632-53b4-410f-b459-8974792add79', 377, 'SYF202000080_ld', '高密度脂蛋白胆固醇校准品', '2x3mL', '贝克曼Ire', '盒', 0.0000, 1, 0, '202008250032', 1, 0, 0, '2020-08-25 10:08:09', 0, '2020-11-10 14:05:00', '不良反应', NULL, '2020-08-25', '2021-08-25');
INSERT INTO `pub_refund_dtl` VALUES (1298079687625510913, 1, 1298079687264800769, 0, 0, 0, '', 'ef1a2f80-fb87-4f83-8e0a-694bf8787a94', 377, 'SYF202000080_ld', '高密度脂蛋白胆固醇校准品', '2x3mL', '贝克曼Ire', '盒', 0.0000, 1, 0, '202008250032', 1, 0, 0, '2020-08-25 10:08:09', 0, '2020-11-10 14:05:02', '包装污染', NULL, '2020-08-25', '2022-08-25');
INSERT INTO `pub_refund_dtl` VALUES (1298084946565500929, 1, 1298084946515169281, 0, 0, 0, '', '0994895e-7d72-4d6a-a8c6-a92e9441e5da', 377, 'SYF202000080_ld', '高密度脂蛋白胆固醇校准品', '2x3mL', '贝克曼Ire', '盒', 0.0000, 1, 0, '4444', 1, 0, 0, '2020-08-25 10:29:03', 0, '2020-11-10 14:05:04', '商品破损', NULL, '2020-08-25', '2021-08-25');
INSERT INTO `pub_refund_dtl` VALUES (1298153523687546882, 1, 1298153523066789890, 0, 0, 0, '', '3c927e6c-2d3a-4d1d-896c-bad07e5a4467', 377, 'SYF202000080_ld', '高密度脂蛋白胆固醇校准品', '2x3mL', '贝克曼Ire', '盒', 0.0000, 1, 0, '4444', 1, 0, 0, '2020-08-25 15:01:33', 0, '2020-11-10 14:05:07', '包装污染', NULL, '2020-08-25', '2020-09-25');
INSERT INTO `pub_refund_dtl` VALUES (1298160067460292610, 1, 1298160067363823618, 0, 0, 0, '', '95fc2560-7010-480e-bab3-9e2c00db3bb6', 380, 'SYF202000001_cw', '总蛋白测定试剂盒（双缩脲法）', 'R1 : 4 x 48mL+R2 : 4 x 48mL', '贝克曼库尔特实验系统(苏州)有限公司', '盒', 0.0000, 1, 0, '202008251526', 1, 0, 0, '2020-08-25 15:27:33', 0, '2020-11-10 14:05:10', '包装污染', NULL, '2020-08-25', '2020-09-25');

-- ----------------------------
-- Table structure for pub_singlecode
-- ----------------------------
DROP TABLE IF EXISTS `pub_singlecode`;
CREATE TABLE `pub_singlecode`  (
  `singlecode` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '单品码',
  `rfid` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT 'RFID',
  `organ_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '机构ID',
  `lot_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '批号ID',
  `batch_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '批次ID',
  `goods_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '商品ID',
  `supply_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '供应商ID',
  `source_type` tinyint(4) NOT NULL DEFAULT 0 COMMENT '来源类型',
  `source_id` bigint(4) NOT NULL DEFAULT 0 COMMENT '来源ID',
  `source_dtl_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '来源明细ID',
  `creater_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '创建人id',
  `gmt_create` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `modifier_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '修改人id',
  `gmt_modified` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  `goods_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '商品名称',
  `goods_spec` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '商品规格',
  `goods_unit` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '商品单位',
  `goods_factory_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '厂家',
  `goods_forecast_days` decimal(10, 0) NULL DEFAULT NULL COMMENT '商品效期预警天数',
  `batch_no` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '批次',
  `price` double(16, 4) NULL DEFAULT 0.0000 COMMENT '单价',
  `lot_no` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '批号',
  `pprod_date` date NULL DEFAULT NULL COMMENT '生产日期',
  `pvalid_date` date NULL DEFAULT NULL COMMENT '有效日期至',
  `goods_code` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '商品编码',
  `supply_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '供应商名称',
  PRIMARY KEY (`singlecode`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '单品码定义表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_singlecode
-- ----------------------------
INSERT INTO `pub_singlecode` VALUES ('E00401081F40EE88', 'E00401081F40EE88', 1, 1273184650732896258, 1273184650774839298, 348, 37, 1, -1184235518, 1273184650766450689, 0, '2020-06-17 17:24:44', 0, '2020-06-17 17:24:44', '肌酸激酶MB同工酶测定试剂盒-CK-MB', '7060     R1 2*80ml   R2 2*20ml', '盒', '北京利德曼生化股份有限公司', 100, '202006170015', 220.0000, '0617-020', '2019-12-16', '2020-12-17', 'G202006001', '南宁乾美商贸有限公司');
INSERT INTO `pub_singlecode` VALUES ('E00401081F40F10C', 'E00401081F40F10C', 1, 1273184650732896258, 1273184650774839298, 348, 37, 1, -1184235518, 1273184650766450689, 0, '2020-06-17 17:24:44', 0, '2020-06-17 17:24:44', '肌酸激酶MB同工酶测定试剂盒-CK-MB', '7060     R1 2*80ml   R2 2*20ml', '盒', '北京利德曼生化股份有限公司', 100, '202006170015', 220.0000, '0617-020', '2019-12-16', '2020-12-17', 'G202006001', '南宁乾美商贸有限公司');
INSERT INTO `pub_singlecode` VALUES ('E00401081F40F379', 'E00401081F40F379', 1, 1273155040196558849, 1273155040251084802, 352, 42, 1, 2099904513, 1273155040242696193, 0, '2020-06-17 16:29:35', 0, '2020-06-17 16:29:35', 'γ-谷氨酰氨基转移酶测定试剂盒', 'R1: 4×40mL+R2: 4×40mL；', '盒', '贝克曼库尔特实验系统(苏州)有限公司', 120, '202006170013', 1000.0000, '0617-015', '2019-12-09', '2020-12-16', 'G20200615', '广州市伟之晨科仪器械有限公司');
INSERT INTO `pub_singlecode` VALUES ('E00401081F40F606', 'E00401081F40F606', 1, 1273155040196558849, 1273155040251084802, 352, 42, 1, 2099904513, 1273155040242696193, 0, '2020-06-17 16:29:35', 0, '2020-06-17 16:29:35', 'γ-谷氨酰氨基转移酶测定试剂盒', 'R1: 4×40mL+R2: 4×40mL；', '盒', '贝克曼库尔特实验系统(苏州)有限公司', 120, '202006170013', 1000.0000, '0617-015', '2019-12-09', '2020-12-16', 'G20200615', '广州市伟之晨科仪器械有限公司');
INSERT INTO `pub_singlecode` VALUES ('E00401081F40F89F', 'E00401081F40F89F', 1, 1273155040196558849, 1273155040251084802, 352, 42, 1, 2099904513, 1273155040242696193, 0, '2020-06-17 16:29:34', 0, '2020-06-17 16:29:34', 'γ-谷氨酰氨基转移酶测定试剂盒', 'R1: 4×40mL+R2: 4×40mL；', '盒', '贝克曼库尔特实验系统(苏州)有限公司', 120, '202006170013', 1000.0000, '0617-015', '2019-12-09', '2020-12-16', 'G20200615', '广州市伟之晨科仪器械有限公司');
INSERT INTO `pub_singlecode` VALUES ('E00401081F40FC80', 'E00401081F40FC80', 1, 1273160868995661825, 1273160869037604866, 348, 36, 1, -1670774782, 1273160869033410561, 0, '2020-06-17 16:29:22', 0, '2020-06-17 16:29:22', '肌酸激酶MB同工酶测定试剂盒-CK-MB', '7060     R1 2*80ml   R2 2*20ml', '盒', '北京利德曼生化股份有限公司', 100, '202006170014', 220.0000, '0617-016', '2020-01-14', '2021-02-17', 'G202006001', '广西信禾通医疗投资有限公司');
INSERT INTO `pub_singlecode` VALUES ('E00401081F40FF13', 'E00401081F40FF13', 1, 1273147782771384322, 1273147782804938754, 350, 36, 1, -1138089983, 1273147782800744449, 0, '2020-06-17 15:31:03', 0, '2020-06-17 15:31:03', '纤维连接蛋白检测试剂盒', 'R1:45ML*2', '盒', '重庆中元生物技术有限公司', 0, '202006170011', 600.0000, '0617-12', '2019-08-13', '2021-01-21', 'G202006003', '广西信禾通医疗投资有限公司');
INSERT INTO `pub_singlecode` VALUES ('E00401081F4101A3', 'E00401081F4101A3', 1, 1273147782771384322, 1273147782804938754, 350, 36, 1, -1138089983, 1273147782800744449, 0, '2020-06-17 15:31:03', 0, '2020-06-17 15:31:03', '纤维连接蛋白检测试剂盒', 'R1:45ML*2', '盒', '重庆中元生物技术有限公司', 0, '202006170011', 600.0000, '0617-12', '2019-08-13', '2021-01-21', 'G202006003', '广西信禾通医疗投资有限公司');
INSERT INTO `pub_singlecode` VALUES ('E00401081F410430', 'E00401081F410430', 1, 1273147782771384322, 1273147782804938754, 350, 36, 1, -1138089983, 1273147782800744449, 0, '2020-06-17 15:31:03', 0, '2020-06-17 15:31:03', '纤维连接蛋白检测试剂盒', 'R1:45ML*2', '盒', '重庆中元生物技术有限公司', 0, '202006170011', 600.0000, '0617-12', '2019-08-13', '2021-01-21', 'G202006003', '广西信禾通医疗投资有限公司');
INSERT INTO `pub_singlecode` VALUES ('E00401081F4106AB', 'E00401081F4106AB', 1, 1273145056473784321, 1273145056515727361, 348, 35, 1, -123068414, 1273145056511533058, 0, '2020-06-17 14:47:02', 0, '2020-06-17 14:47:02', '肌酸激酶MB同工酶测定试剂盒-CK-MB', '7060     R1 2*80ml   R2 2*20ml', '盒', '北京利德曼生化股份有限公司', 100, '202006170010', 220.0000, '0617-011', '2019-12-24', '2021-03-24', 'G202006001', '广西真世好生物科技');
INSERT INTO `pub_singlecode` VALUES ('E00401081F410A5F', 'E00401081F410A5F', 1, 1273141198414553090, 1273141198578130946, 348, 32, 1, -1221976062, 1273141198565548034, 0, '2020-06-17 14:32:25', 0, '2020-06-17 14:32:25', '肌酸激酶MB同工酶测定试剂盒-CK-MB', '7060     R1 2*80ml   R2 2*20ml', '盒', '北京利德曼生化股份有限公司', 100, '202006170009', 220.0000, '0617-010', '2019-10-14', '2021-01-18', 'G202006001', '广西慧篆商贸有限公司');
INSERT INTO `pub_singlecode` VALUES ('E00401081F410CD2', 'E00401081F410CD2', 1, 1273141198414553090, 1273141198578130946, 348, 32, 1, -1221976062, 1273141198565548034, 0, '2020-06-17 14:32:25', 0, '2020-06-17 14:32:25', '肌酸激酶MB同工酶测定试剂盒-CK-MB', '7060     R1 2*80ml   R2 2*20ml', '盒', '北京利德曼生化股份有限公司', 100, '202006170009', 220.0000, '0617-010', '2019-10-14', '2021-01-18', 'G202006001', '广西慧篆商贸有限公司');

-- ----------------------------
-- Table structure for pub_stock
-- ----------------------------
DROP TABLE IF EXISTS `pub_stock`;
CREATE TABLE `pub_stock`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '库存id',
  `organ_id` bigint(20) NULL DEFAULT 0 COMMENT '机构id',
  `storehouse_id` bigint(20) NULL DEFAULT 0 COMMENT '库房id',
  `zone_id` bigint(20) NULL DEFAULT 0 COMMENT '区域id',
  `loc_id` bigint(20) NULL DEFAULT 0 COMMENT '货位id',
  `batch_id` bigint(20) NULL DEFAULT 0 COMMENT '批次id',
  `goods_id` bigint(20) NULL DEFAULT 0 COMMENT '商品id',
  `plot_id` bigint(20) NULL DEFAULT 0 COMMENT '批号id',
  `quality_status` int(1) NULL DEFAULT 0 COMMENT '质量状态(0:合格;1:不合格)',
  `goods_status` int(1) NULL DEFAULT 0 COMMENT '商品状态(0:可配;1:短少;2:锁定)',
  `stock_quantity` decimal(12, 2) NULL COMMENT '库存数量',
  `creater_id` bigint(20) NULL DEFAULT 0 COMMENT '创建人id',
  `gmt_create` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `modifier_id` bigint(20) NULL DEFAULT 0 COMMENT '修改人id',
  `gmt_modified` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 244 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '库存表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_stock
-- ----------------------------
INSERT INTO `pub_stock` VALUES (238, 1, 34, 367, 271, 1273141198578130946, 348, 1273141198414553090, 0, 0, 0.00, 1, '2020-06-17 14:32:26', 1, '2020-06-17 14:32:26');
INSERT INTO `pub_stock` VALUES (239, 1, 34, 367, 271, 1273145056515727361, 348, 1273145056473784321, 0, 0, 0.00, 1, '2020-06-17 14:47:02', 1, '2020-06-17 14:47:02');
INSERT INTO `pub_stock` VALUES (240, 1, 34, 367, 271, 1273147782804938754, 350, 1273147782771384322, 0, 0, 2.00, 1, '2020-06-17 15:31:04', 1, '2020-06-17 15:31:04');
INSERT INTO `pub_stock` VALUES (241, 1, 34, 367, 271, 1273160869037604866, 348, 1273160868995661825, 0, 0, 1.00, 1, '2020-06-17 16:29:22', 1, '2020-06-17 16:29:22');
INSERT INTO `pub_stock` VALUES (242, 1, 34, 0, 0, 1273155040251084802, 352, 1273155040196558849, 0, 0, 2.00, 1, '2020-06-17 16:29:35', 1, '2020-06-17 16:29:35');
INSERT INTO `pub_stock` VALUES (243, 1, 34, 367, 271, 1273184650774839298, 348, 1273184650732896258, 0, 0, 0.00, 1, '2020-06-17 17:24:45', 1, '2020-06-17 17:24:45');

-- ----------------------------
-- Table structure for pub_stock_distribution
-- ----------------------------
DROP TABLE IF EXISTS `pub_stock_distribution`;
CREATE TABLE `pub_stock_distribution`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '可配库存id',
  `organ_id` bigint(20) NULL DEFAULT 0 COMMENT '机构id',
  `storehouse_id` bigint(20) NULL DEFAULT 0 COMMENT '库房id',
  `goods_id` bigint(20) NULL DEFAULT 0 COMMENT '商品id',
  `quality_status` int(1) NULL DEFAULT 0 COMMENT '质量状态(0:合格;1:不合格)',
  `goods_status` int(1) NULL DEFAULT 0 COMMENT '商品状态(0:可配;1:短少;2:锁定)',
  `stock_quantity` decimal(12, 2) NULL COMMENT '库存数量',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 83 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '可配库存表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_stock_distribution
-- ----------------------------
INSERT INTO `pub_stock_distribution` VALUES (80, 1, 34, 348, 0, 0, 3.00);
INSERT INTO `pub_stock_distribution` VALUES (81, 1, 34, 350, 0, 0, 2.00);
INSERT INTO `pub_stock_distribution` VALUES (82, 1, 34, 352, 0, 0, 2.00);

-- ----------------------------
-- Table structure for pub_storehouse
-- ----------------------------
DROP TABLE IF EXISTS `pub_storehouse`;
CREATE TABLE `pub_storehouse`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'id',
  `organ_id` bigint(20) NULL DEFAULT 0 COMMENT '机构id',
  `storehouse_source_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '原库房id',
  `storehouse_code` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '库房编码',
  `storehouse_name` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '库房名称',
  `storehouse_addr` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '库房地址',
  `storehouse_status` tinyint(1) NULL DEFAULT 0 COMMENT '库房状态(0:禁用,1:启用)',
  `storehouse_memo` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '库房备注',
  `department_id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '库房所属部门ID',
  `is_delete` tinyint(1) NULL DEFAULT 0 COMMENT '是否删除(0:否,1:是)',
  `creater_id` bigint(20) NULL DEFAULT 0 COMMENT '创建人id',
  `gmt_create` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `modifier_id` bigint(20) NULL DEFAULT 0 COMMENT '修改人id',
  `gmt_modified` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  `origin_id` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '源数据id',
  `storehouse_type` tinyint(4) NULL DEFAULT 0 COMMENT '库房类型',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_storehouse_no`(`storehouse_code`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 38 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '库房表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_storehouse
-- ----------------------------
INSERT INTO `pub_storehouse` VALUES (34, 1, 'b2c1cacd-b53d-4482-8503-c8cdc96d6da4', '201', '中心库', '0000000013', 1, '1发 ', '0000000000', 0, 0, '2020-06-17 15:50:30', 0, '2020-06-17 15:50:36', '0689f093-d231-406f-986a-da1534528b5a', 5);
INSERT INTO `pub_storehouse` VALUES (35, 1, 'a8a57821-da8e-4cc0-ac72-3b1e12342454', '301', '检验库房', '0000000014', 1, '1发', '0000000002', 0, 0, '2020-06-18 14:22:06', 0, '2020-06-18 14:22:06', '4d294f96-0a0a-4c07-b941-0b68e27c8e6a', 6);
INSERT INTO `pub_storehouse` VALUES (36, 1, '21ec6cf1-4e64-485a-8973-17f74547a802', '402', '冰箱二', '0000000016', 1, '1方法', '0000000004', 0, 0, '2020-06-17 15:46:25', 0, '2020-06-17 15:46:26', '394e84a4-717f-47d2-9484-fe5867f198fc', 7);
INSERT INTO `pub_storehouse` VALUES (37, 1, 'c3c99427-87f9-4c72-8fd8-bbacbde42b30', '401', '冰箱一', '0000000015', 1, '1法师', '0000000003', 0, 0, '2020-06-28 16:44:07', 0, '2020-06-28 16:44:16', 'c1f44d09-9a7f-4745-bffc-365a3f860f9e', 7);

-- ----------------------------
-- Table structure for pub_supply
-- ----------------------------
DROP TABLE IF EXISTS `pub_supply`;
CREATE TABLE `pub_supply`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键id',
  `organ_id` bigint(20) NULL DEFAULT 0 COMMENT '机构id',
  `supply_source_id` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '原供应商id',
  `supply_code` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '供应商编码',
  `supply_name` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '供应商名称',
  `gmt_create` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `gmt_modified` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 43 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '供应商信息' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_supply
-- ----------------------------
INSERT INTO `pub_supply` VALUES (30, 1, '16d51e90-82ea-4463-bfad-37dd9b6bf953', '', '南宁德蓝科技有限公司', '2020-06-10 15:22:00', '2020-06-10 15:22:00');
INSERT INTO `pub_supply` VALUES (31, 1, '1c8be93e-68f4-4b2d-b8c0-ac1c1f2b8000', '', '南宁民嵩医疗器械有限责任公司', '2020-06-12 09:23:00', '2020-06-12 09:23:00');
INSERT INTO `pub_supply` VALUES (32, 1, '38ce5b4b-3cde-4a5b-b03a-be9e1ea3315e', '', '广西慧篆商贸有限公司', '2020-06-12 10:47:00', '2020-06-12 10:47:00');
INSERT INTO `pub_supply` VALUES (33, 1, '45f7e8db-fc66-401b-9a5a-ad1ca636ddd7', '', '南宁万森丹焰贸易有限公司', '2020-06-12 11:49:30', '2020-06-12 11:49:30');
INSERT INTO `pub_supply` VALUES (34, 1, '5b82805f-3e46-4c71-8511-273aa71c365b', '', '广西鼎健医疗器械有限公司', '2020-06-12 14:40:40', '2020-06-12 14:40:40');
INSERT INTO `pub_supply` VALUES (35, 1, '2a135b39-dcc5-4849-af91-8f3c79430c60', '', '广西真世好生物科技', '2020-06-12 17:02:30', '2020-06-12 17:02:30');
INSERT INTO `pub_supply` VALUES (36, 1, '181a2331-baee-4bd3-bd6b-a89e15294ac5', '', '广西信禾通医疗投资有限公司', '2020-06-12 17:54:20', '2020-06-12 17:54:20');
INSERT INTO `pub_supply` VALUES (37, 1, '3a7b1222-4151-44d3-96b1-f0c804e92dc6', '', '南宁乾美商贸有限公司', '2020-06-15 09:53:50', '2020-06-15 09:53:50');
INSERT INTO `pub_supply` VALUES (38, 1, '44359acc-bdd3-4cf5-9530-4df3c9a2e812', '', '广西速康医疗设备有限公司', '2020-06-15 14:14:30', '2020-06-15 14:14:30');
INSERT INTO `pub_supply` VALUES (39, 1, '5b4d66d0-586b-44ac-804f-b8c543be6099', '', '齐童的药厂', '2020-06-15 15:55:40', '2020-06-15 15:55:40');
INSERT INTO `pub_supply` VALUES (40, 1, '3659f54b-6460-44b4-965b-c96c29630139', '', '广西品真供应链管理有限公司', '2020-06-15 16:09:30', '2020-06-15 16:09:30');
INSERT INTO `pub_supply` VALUES (41, 1, '4236facc-ad75-445c-8088-0ac91290ad2a', '', '广州派翠克商贸有限公司', '2020-06-15 16:53:50', '2020-06-15 16:53:50');
INSERT INTO `pub_supply` VALUES (42, 1, '169248cb-2bc5-40d2-bc17-c069293474c3', '', '广州市伟之晨科仪器械有限公司', '2020-06-17 15:26:30', '2020-06-17 15:26:30');

-- ----------------------------
-- Table structure for pub_zone
-- ----------------------------
DROP TABLE IF EXISTS `pub_zone`;
CREATE TABLE `pub_zone`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'id',
  `organ_id` bigint(20) NULL DEFAULT 0 COMMENT '机构id',
  `storehouse_id` bigint(20) NULL DEFAULT NULL COMMENT '所属库房id',
  `zone_code` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '区域编码',
  `zone_type` tinyint(4) NULL DEFAULT 0 COMMENT '区域类型(0:正常,1:待验,2:暂存,3:不合格,4:退货)',
  `zone_addr` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '' COMMENT '区域位置',
  `zone_storage_nature` tinyint(4) NULL DEFAULT 0 COMMENT '区域存储属性(0:常温,1:阴凉,2:冷藏,3:冷冻)',
  `zone_status` tinyint(4) NULL DEFAULT 0 COMMENT '区域状态(0:禁用,1:启用)',
  `zone_memo` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '区域备注',
  `is_delete` tinyint(1) NULL DEFAULT 0 COMMENT '是否删除(0:否,1:是)',
  `creater_id` bigint(20) NULL DEFAULT 0 COMMENT '创建人id',
  `gmt_create` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `modifier_id` bigint(20) NULL DEFAULT 0 COMMENT '修改人id',
  `gmt_modified` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_storehouse_id`(`storehouse_id`) USING BTREE,
  INDEX `idx_zone_code`(`zone_code`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 372 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '区域表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_zone
-- ----------------------------
INSERT INTO `pub_zone` VALUES (350, 1, 25, 'ZoneC001', 3, '', 0, 1, NULL, 1, 0, '2020-06-10 14:13:26', 0, '2020-06-10 14:48:36');
INSERT INTO `pub_zone` VALUES (351, 1, 26, 'ZoneL001', 3, '', 0, 1, NULL, 1, 0, '2020-06-10 14:15:56', 0, '2020-06-10 14:48:56');
INSERT INTO `pub_zone` VALUES (352, 1, 27, 'ZoneL002', 3, '', 0, 1, NULL, 1, 0, '2020-06-10 14:16:56', 0, '2020-06-10 14:48:46');
INSERT INTO `pub_zone` VALUES (353, 1, 28, 'ZoneC004', 3, '', 0, 1, NULL, 1, 0, '2020-06-10 14:18:46', 0, '2020-06-10 14:48:46');
INSERT INTO `pub_zone` VALUES (354, 1, 29, 'ZoneC0000', 3, '', 0, 1, NULL, 1, 0, '2020-06-10 14:28:36', 0, '2020-06-10 14:48:36');
INSERT INTO `pub_zone` VALUES (355, 1, 30, 'Zone201', 3, '', 0, 1, NULL, 1, 0, '2020-06-10 14:32:46', 0, '2020-06-12 15:52:00');
INSERT INTO `pub_zone` VALUES (356, 1, 31, 'Zone301', 3, '', 0, 1, NULL, 1, 0, '2020-06-10 14:34:46', 0, '2020-06-12 15:51:56');
INSERT INTO `pub_zone` VALUES (357, 1, 32, 'Zone401', 3, '', 0, 1, NULL, 1, 0, '2020-06-10 14:35:26', 0, '2020-06-12 15:51:54');
INSERT INTO `pub_zone` VALUES (358, 1, 33, 'Zone402', 3, '', 0, 1, NULL, 1, 0, '2020-06-10 14:36:36', 0, '2020-06-12 15:51:53');
INSERT INTO `pub_zone` VALUES (359, 1, 30, 'A001', 0, '一个新的总库房', 0, 0, '一个两个三个四个', 1, 0, '2020-06-11 09:33:53', 0, '2020-06-12 15:51:51');
INSERT INTO `pub_zone` VALUES (360, 1, 18, 'A002', 1, '墙角', 1, 0, '00', 1, 0, '2020-06-11 11:51:30', 0, '2020-06-12 15:51:50');
INSERT INTO `pub_zone` VALUES (361, 1, 19, 'A003', 4, '', 0, 0, NULL, 1, 0, '2020-06-11 11:52:18', 0, '2020-06-12 15:51:48');
INSERT INTO `pub_zone` VALUES (362, 1, 23, 'A004', 2, '', 1, 0, NULL, 1, 0, '2020-06-11 11:53:06', 0, '2020-06-11 11:53:30');
INSERT INTO `pub_zone` VALUES (363, 1, 12, 'A001', 0, '', 0, 0, NULL, 1, 0, '2020-06-11 14:56:17', 0, '2020-06-12 15:51:45');
INSERT INTO `pub_zone` VALUES (364, 1, 13, '6546546456464ds6f4a64f66sda4f6', 0, '安静的类似的积分拉时发觉上当了假发附件ADSL库房间拉丝机发生间段付款啦司法解释快乐的解放路卡斯加法伤接待来访发生家乐福', 0, 0, NULL, 1, 0, '2020-06-11 14:59:56', 0, '2020-06-12 15:51:42');
INSERT INTO `pub_zone` VALUES (365, 1, 34, 'B-01', 0, '二七', 0, 0, NULL, 0, 0, '2020-06-11 15:34:28', 0, '2020-06-12 15:50:12');
INSERT INTO `pub_zone` VALUES (366, 1, 34, 'B-02', 3, '区委', 1, 0, '任务', 0, 0, '2020-06-11 15:34:28', 0, '2020-06-12 15:50:07');
INSERT INTO `pub_zone` VALUES (367, 1, 34, 'Zone201', 0, '发送到', 2, 1, '发', 0, 0, '2020-06-12 09:52:56', 0, '2020-06-15 10:47:38');
INSERT INTO `pub_zone` VALUES (368, 1, 35, 'Zone301', 3, '', 0, 1, NULL, 0, 0, '2020-06-12 09:53:06', 0, '2020-06-12 09:53:06');
INSERT INTO `pub_zone` VALUES (369, 1, 36, 'Zone402', 0, '', 2, 1, NULL, 0, 0, '2020-06-12 09:53:16', 0, '2020-06-15 10:47:19');
INSERT INTO `pub_zone` VALUES (370, 1, 37, 'Zone401', 0, '', 2, 1, NULL, 0, 0, '2020-06-12 09:53:26', 0, '2020-06-15 10:47:05');
INSERT INTO `pub_zone` VALUES (371, 1, 34, 'B101', 0, '', 0, 0, NULL, 0, 0, '2020-06-12 15:44:15', 0, '2020-06-12 15:44:15');

-- ----------------------------
-- Table structure for userinfo
-- ----------------------------
DROP TABLE IF EXISTS `userinfo`;
CREATE TABLE `userinfo`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `LoginName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Pwd` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `UserStatus` int(11) NULL DEFAULT NULL,
  `Remark` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DelFlag` tinyint(1) NULL DEFAULT NULL,
  `RoleInfoID` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of userinfo
-- ----------------------------
INSERT INTO `userinfo` VALUES (1, 'admin', 'xXjfuiPQpFE=', 'admin', 0, NULL, NULL, NULL);

-- ----------------------------
-- View structure for viewcargodetail
-- ----------------------------
DROP VIEW IF EXISTS `viewcargodetail`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `viewcargodetail` AS select `cargoinfo`.`CargoName` AS `CargoName`,`cargodetail`.`ID` AS `ID`,`cargodetail`.`CID` AS `CID`,`cargodetail`.`ProductNo` AS `ProductNo`,`cargodetail`.`ProductName` AS `ProductName`,`cargodetail`.`ProductType` AS `ProductType`,`cargodetail`.`FactoryName` AS `FactoryName`,`cargodetail`.`Unit` AS `Unit`,`cargodetail`.`OutputNum` AS `OutputNum`,`cargodetail`.`CheckNum` AS `CheckNum`,`cargodetail`.`Price` AS `Price`,`cargodetail`.`BatchNo` AS `BatchNo`,`cargodetail`.`CreateDate` AS `CreateDate`,`cargodetail`.`VerifyDate` AS `VerifyDate` from (`cargodetail` join `cargoinfo` on((`cargodetail`.`CID` = `cargoinfo`.`ID`)));

-- ----------------------------
-- View structure for viewcargoinfo
-- ----------------------------
DROP VIEW IF EXISTS `viewcargoinfo`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `viewcargoinfo` AS select `cargoinfo`.`ID` AS `ID`,`cargoinfo`.`SortNo` AS `SortNo`,`cargoinfo`.`CargoName` AS `CargoName`,`cargoinfo`.`SupplierName` AS `SupplierName`,`cargoinfo`.`OutputName` AS `OutputName`,`cargoinfo`.`Operator` AS `Operator` from `cargoinfo`;

-- ----------------------------
-- View structure for viewprofuctinfo
-- ----------------------------
DROP VIEW IF EXISTS `viewprofuctinfo`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `viewprofuctinfo` AS select `productinfo`.`ID` AS `ID`,`productinfo`.`BID` AS `BID`,`productinfo`.`ProductNo` AS `ProductNo`,`productinfo`.`ProductName` AS `ProductName`,`productinfo`.`ProductType` AS `ProductType`,`productinfo`.`FactoryName` AS `FactoryName`,`productinfo`.`Unit` AS `Unit`,`productinfo`.`VerifyNum` AS `VerifyNum`,`productinfo`.`CheckNum` AS `CheckNum`,`productinfo`.`Price` AS `Price`,`productinfo`.`BatchNo` AS `BatchNo`,`productinfo`.`CreateDate` AS `CreateDate`,`productinfo`.`VerifyDate` AS `VerifyDate`,`billsinfo`.`BillsName` AS `BillsName` from (`productinfo` join `billsinfo` on((`productinfo`.`BID` = `billsinfo`.`ID`)));

-- ----------------------------
-- View structure for view_pub_accept
-- ----------------------------
DROP VIEW IF EXISTS `view_pub_accept`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `view_pub_accept` AS select `a`.`id` AS `id`,`a`.`organ_id` AS `organ_id`,`a`.`source_type` AS `source_type`,`a`.`source_id` AS `source_id`,`a`.`source_no` AS `source_no`,`a`.`origin_id` AS `origin_id`,`a`.`supply_id` AS `supply_id`,`a`.`supply_name` AS `supply_name`,`a`.`supply_time` AS `supply_time`,`a`.`storehouse_id` AS `storehouse_id`,`a`.`accept_person_id` AS `accept_person_id`,`a`.`accept_person_name` AS `accept_person_name`,`a`.`check_person_id` AS `check_person_id`,`a`.`check_person_name` AS `check_person_name`,`a`.`status` AS `status`,`a`.`is_delete` AS `is_delete`,`a`.`creater_id` AS `creater_id`,`a`.`gmt_create` AS `gmt_create`,`a`.`modifier_id` AS `modifier_id`,`a`.`gmt_modified` AS `gmt_modified`,`o`.`organ_name` AS `organ_name`,`s`.`storehouse_name` AS `storehouse_name` from ((`pub_accept` `a` left join `pub_organ` `o` on((`a`.`organ_id` = `o`.`id`))) left join `pub_storehouse` `s` on((`a`.`storehouse_id` = `s`.`id`)));

SET FOREIGN_KEY_CHECKS = 1;
