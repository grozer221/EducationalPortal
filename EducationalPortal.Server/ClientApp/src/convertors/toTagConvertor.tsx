import {SubjectPostType} from '../areas/teacher-area/modules/subjectPosts/subjectPosts.types';
import {Tag} from 'antd';
import {BookOutlined, InfoCircleOutlined} from '@ant-design/icons';
import {Role} from '../areas/teacher-area/modules/users/users.types';

export const subjectPostTypeToTag = (subjectPostType: SubjectPostType) => {
    switch (subjectPostType) {
        case SubjectPostType.Homework:
            return (<Tag color="red"><BookOutlined/></Tag>);
        case SubjectPostType.Info:
            return (<Tag color="blue"><InfoCircleOutlined/></Tag>);
    }
};

export const roleToTag = (role: Role) => {
    const roleString = Object.keys(Role)[Object.values(Role).indexOf(role)];
    switch (role) {
        case Role.Student:
            return (<Tag color="red">{roleString}</Tag>);
        case Role.Teacher:
            return (<Tag color="blue">{roleString}</Tag>);
        case Role.Administrator:
            return (<Tag color="purple">{roleString}</Tag>);
    }
};
