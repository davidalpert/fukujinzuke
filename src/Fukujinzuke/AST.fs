namespace Fukujinzuke.AST

type Keyword =
| Feature

type FeatureTitle = FeatureTitle of string
type FeatureDescription = FeatureDescription of string

type FeatureHeader = FeatureHeader of FeatureTitle * FeatureDescription

type FeatureFile = FeatureFile of FeatureHeader
