export interface BuildVersion {
    id: number;
    projectName: string;
    major: number;
    minor: number;
    build: number;
    revision: number;
    semanticVersionText: string;
    version: string;
    release: string;
    semanticVersion: string;
    semanticRelease: string;
}
