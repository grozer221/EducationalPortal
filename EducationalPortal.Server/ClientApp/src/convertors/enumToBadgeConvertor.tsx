import {SubjectPostType} from '../areas/teacher-area/modules/subjectPosts/subjectPosts.types';
import {Tag} from 'antd';
import {BookOutlined, InfoCircleOutlined} from '@ant-design/icons';

export const subjectPostTypeToTag = (subjectPostType: SubjectPostType) => {
    switch (subjectPostType) {
        case SubjectPostType.Homework:
            return (<Tag color="red"><BookOutlined/></Tag>);
        case SubjectPostType.Info:
            return (<Tag color="blue"><InfoCircleOutlined/></Tag>);
    }
};
